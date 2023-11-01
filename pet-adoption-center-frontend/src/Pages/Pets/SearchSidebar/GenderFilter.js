import React, { useState } from "react";

function GenderFilter({ onChange }) {
  const [selectedGender, setSelectedGender] = useState("");

  const handleGenderChange = (event) => {
    const gender = event.target.value;
    setSelectedGender(gender);

    let genderValue = 3;

    if (gender === "male") {
      genderValue = 0;
    } else if (gender === "female") {
      genderValue = 1;
    }

    // Wywołaj funkcję onChange i przekaż wybraną wartość płci (0 lub 1)
    onChange(genderValue);
  };

  return (
    <div className="filter">
      <h3>Filter by Gender</h3>
      <select value={selectedGender} onChange={handleGenderChange}>
        <option value="">Select a gender</option>
        <option value="male">Male</option>
        <option value="female">Female</option>
      </select>
    </div>
  );
}

export default GenderFilter;

