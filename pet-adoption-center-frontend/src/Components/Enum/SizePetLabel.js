import React from 'react';

const SizePetLabel = (size) => {
  if (size === 0) {
    return 'Small';
  } else if (size === 1) {
    return 'Medium';
  } else if (size == 2) {
    return 'Large';
  }
  return 'Unknown';
};

export default SizePetLabel;
