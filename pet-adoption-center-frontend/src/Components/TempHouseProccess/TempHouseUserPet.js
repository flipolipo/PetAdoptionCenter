import React from 'react';
import { useParams } from 'react-router-dom'
import PetById from '../../Pages/Pets/PetsById/PetById';

const TempHouseUserPet = () => {
    const { tempHouseId, petId, userId } = useParams();
   /*  console.log(tempHouseId);
    console.log(petId);
    console.log(userId);
 */
  return (
    <div><PetById petTempHouseId={petId} userTempHouseId={userId} tempHouseId={tempHouseId}/></div>
  )
}

export default TempHouseUserPet