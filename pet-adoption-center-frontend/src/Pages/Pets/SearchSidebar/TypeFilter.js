import React, { useState } from "react";

function TypeFilter({ onChange }) {
  const [selectedType, setSelectedType] = useState("");

  const handleTypeChange = (event) => {
    const type = event.target.value;
    setSelectedType(type);

    let typeValue = -1;

    if (type === "dog") {
      typeValue = 0;
    } else if (type === "cat") {
      typeValue = 1;
    }

    onChange(typeValue);
  };

  return (
    <div className="filter">
      <h3>Filter by Type</h3>
      <select value={selectedType} onChange={handleTypeChange}>
        <option value="">Select a type</option>
        <option value="cat">Cat</option>
        <option value="dog">Dog</option>
      </select>
    </div>
  );
}

export default TypeFilter;

