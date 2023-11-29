import React, { useState } from "react";
import Select from "react-select";

function TypeFilter({ onChange }) {
  const typeOptions = [
    { value: "", label: "Select a type" },
    { value: 0, label: "Dog" },
    { value: 1, label: "Cat" },
  ];

  const [selectedType, setSelectedType] = useState("");

  const handleTypeChange = (selectedOption) => {
    setSelectedType(selectedOption);
    onChange(selectedOption ? selectedOption.value : "");
  };

  return (
    <div className="filter">
      <h3>Filter by Type</h3>
      <Select
        value={selectedType}
        onChange={handleTypeChange}
        options={typeOptions}
        className="select-type"
        placeholder="Select type"
        isClearable
      />
    </div>
  );
}

export default TypeFilter;
