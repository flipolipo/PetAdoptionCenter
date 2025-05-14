import React from 'react'

const TypePetLabel = (type) => {
    if (type === 0) {
        return 'Dog';
      } else if (type === 1) {
        return 'Cat';
      }
      return 'Unknown';
}

export default TypePetLabel