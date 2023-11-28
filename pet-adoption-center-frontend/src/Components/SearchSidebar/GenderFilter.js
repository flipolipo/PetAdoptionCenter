import React, { useState } from "react";
import Select from "react-select";

function GenderFilter({ onChange }) {
  const genderOptions = [
    { value: "", label: "Select a gender" },
    { value: 0, label: "Male" },
    { value: 1, label: "Female" },
  ];

  const [selectedGender, setSelectedGender] = useState("");

  const handleGenderChange = (selectedOption) => {
    setSelectedGender(selectedOption);
    onChange(selectedOption ? selectedOption.value : "");
  };
  

  return (
    <div className="filter">
      <h3>Filter by Gender</h3>
      <Select
        value={selectedGender}
        onChange={handleGenderChange}
        options={genderOptions}
        className="select-gender"
        placeholder="Select gender"
        isClearable
      />
    </div>
  );
}

export default GenderFilter;
