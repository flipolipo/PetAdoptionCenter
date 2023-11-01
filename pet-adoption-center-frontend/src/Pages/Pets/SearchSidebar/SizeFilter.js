import React, { useState } from "react";

function SizeFilter({ onChange }) {
  const [selectedSize, setSelectedSize] = useState("");

  const handleSizeChange = (event) => {
    const size = event.target.value;
    setSelectedSize(size);

    let sizeValue = -1;

    if (size === "small") {
      sizeValue = 0;
    } else if (size === "medium") {
      sizeValue = 1;
    } else if (size === "large") {
      sizeValue = 2;
    }

    onChange(sizeValue);
  };

  return (
    <div className="filter">
      <h3>Filter by Size</h3>
      <select value={selectedSize} onChange={handleSizeChange}>
        <option value="">Select a size</option>
        <option value="small">Small</option>
        <option value="medium">Medium</option>
        <option value="large">Large</option>
      </select>
    </div>
  );
}

export default SizeFilter;

