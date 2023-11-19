import React, { useState } from "react";
import Select from "react-select";

function SizeFilter({ onChange }) {
  const sizeOptions = [
    { value: "", label: "Select a size" },
    { value: 0, label: "Small" },
    { value: 1, label: "Medium" },
    { value: 2, label: "Large" },
  ];

  const [selectedSize, setSelectedSize] = useState("");

  const handleSizeChange = (selectedOption) => {
    setSelectedSize(selectedOption);
    onChange(selectedOption.value);
  };

  return (
    <div className="filter">
      <h3>Filter by Size</h3>
      <Select
        value={selectedSize}
        onChange={handleSizeChange}
        options={sizeOptions}
        className="select-size"
        placeholder="select size"
      />
    </div>
  );
}

export default SizeFilter;