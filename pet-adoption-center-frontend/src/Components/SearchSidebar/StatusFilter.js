import React, { useState } from "react";
import Select from "react-select";

function StatusFilter({ onChange }) {
  const statusOptions = [
    { value: "", label: "Select a status" },
    { value: 0, label: "Temporary House" },
    { value: 1, label: "At Shelter" },
    { value: 3, label: "Adopted" },
    { value: 4, label: "On Adoption Processs" },
  ];

  const [selectedStatus, setSelectedStatus] = useState("");

  const handleStatusChange = (selectedOption) => {
    setSelectedStatus(selectedOption);
    onChange(selectedOption.value);
  };

  return (
    <div className="filter">
      <h3>Filter by Status</h3>
      <Select
        value={selectedStatus}
        onChange={handleStatusChange}
        options={statusOptions}
        className="select-status"
        placeholder="select status"
      />
    </div>
  );
}

export default StatusFilter;