import React, { useState, useEffect } from "react";
import ReactSelect from "react-select";
import {  saveProcedureToUser } from "../../../api/api";

const PlanProcedureItem = ({ procedure, users, planId ,userprocedures}) => {
  const [selectedUsers, setSelectedUsers] = useState([]);
  useEffect(() => {
    const fetchFilteredUsers = async () => {
      const filteredUsers = await getFilteredUsers(
        users,
        procedure.procedureId,
        planId,
        userprocedures
      );
      setSelectedUsers(filteredUsers);
    };

    fetchFilteredUsers();
  }, [planId,users,procedure.procedureId,userprocedures]);

  const getFilteredUsers = async (users, procedureId, planId,userprocedures) => {

    return users.map((user) => {
      const hasMatchingProcedure = userprocedures.some(
        (p) =>
          p.procedureId === procedureId &&
          p.userId === user.value &&
          p.planId === Number(planId)
      );

      return hasMatchingProcedure
        ? { label: user.label, value: user.value }
        : null;
    });
  };

  const handleAssignUserToProcedure = async (userdetails, procedureId) => {
    if (!userdetails || userdetails.length === 0) {
        setSelectedUsers([]);
        return;
    }

    const allFilteredUsers = [];

    for (const user of userdetails) {
        const userId = user.value;
        const userName = user.label;

        if (userId !== null) {
            const filteredUsers = await getFilteredUsers([user], procedureId, planId,userprocedures);

            if (filteredUsers.length > 0 && filteredUsers.find((filteredUser) => filteredUser !== null && filteredUser.value === userId)) {
                allFilteredUsers.push(...filteredUsers.filter(Boolean));
            } else {
                allFilteredUsers.push({ label: userName, value: userId });
                await saveProcedureToUser(planId, userId, userName, procedureId);
            }
        }
    }

    setSelectedUsers(allFilteredUsers);
};

  return (
    <div className="py-2">
      <div>{procedure.procedureTitle}</div>

      <ReactSelect
        className="mt-2"
        placeholder="Select User to Assign"
        isMulti={true}
        options={users}
        value={selectedUsers}
        getOptionLabel={(users) => users.label}
        getOptionValue={(users) => users.value}
        onChange={(users) =>
          handleAssignUserToProcedure(users, procedure.procedureId)
        }
      />
    </div>
  );
};

export default PlanProcedureItem;
