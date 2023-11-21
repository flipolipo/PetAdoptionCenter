import React, { useState } from "react";
import Select from "react-select";

function TypeFilter({ onChange }) {
  const typeOptions = [
    { value: "", label: "Select a type" },
    { value: 0, label: "Cat" },
    { value: 1, label: "Dog" },
  ];

  const [selectedType, setSelectedType] = useState("");

  const handleTypeChange = (selectedOption) => {
    setSelectedType(selectedOption);
    onChange(selectedOption.value);
  };

  return (
    <div className="filter">
      <h3>Filter by Type</h3>
      <Select
        value={selectedType}
        onChange={handleTypeChange}
        options={typeOptions}
        className="select-type"
        placeholder="select type"
      />
    </div>
  );
}

export default TypeFilter;