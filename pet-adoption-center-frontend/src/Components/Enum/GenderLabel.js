import React from "react";

const GenderPetLabel = (gender) => {
    if (gender === 0) {
      return 'Male';
    } else if (gender === 1) {
      return 'Female';
    }
    return 'Unknown';
  };
export default GenderPetLabel