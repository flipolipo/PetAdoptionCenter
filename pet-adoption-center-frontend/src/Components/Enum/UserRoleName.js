import React from 'react'

const UserRoleName = (role) => {
    if (role === 0) {
        return 'Shelter Owner';
      } else if (role === 1) {
        return 'Shelter Worker';
      } else if (role === 2) {
        return 'Contributor';
      } else if (role === 3) {
        return 'Adopter';
      } else if (role === 4) {
        return 'User';
      } else if (role === 5) {
        return 'Temporary House Owner';
      }
      return 'Unknown';
}

export default UserRoleName