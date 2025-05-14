import React from 'react'

const StatusPetLabel = (status) => {
    if (status === 0) {
        return 'In Temporary House';
      } else if (status === 1) {
        return 'At Shelter';
      } else if (status === 2) {
        return 'On A Walk';
      } else if (status === 3) {
        return 'Adopted';
      } else if (status === 4) {
        return 'On Adoption Proccess';
      } else if (status === 5){
        return 'On Temporary House Proccess'
      }
      return 'Unknown';
}

export default StatusPetLabel