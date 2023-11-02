import React, { useState } from "react";

function StatusFilter({ onChange }) {
  const [selectedStatus, setSelectedStatus] = useState("");

  const handlestatusChange = (event) => {
    const status = event.target.value;
    setSelectedStatus(status);

    let statusValue = -1;

    if (status === "TemporaryHouse") {
      statusValue = 0;
    } else if (status === "AtShelter") {
      statusValue = 1;
    } else if (status === "Adopted") {
      statusValue = 3;
    }

    onChange(statusValue);
  };

  return (
    <div className="filter">
      <h3>Filter by status</h3>
      <select value={selectedStatus} onChange={handlestatusChange}>
        <option value="">Select a status</option>
        <option value="TemporaryHouse">Temporary House</option>
        <option value="AtShelter">At Shelter</option>
        <option value="Adopted">Adopted</option>
      </select>
    </div>
  );
}

export default StatusFilter;