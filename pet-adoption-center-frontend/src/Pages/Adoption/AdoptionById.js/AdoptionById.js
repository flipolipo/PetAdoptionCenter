import React from 'react'
import { useParams } from 'react-router-dom'
import PetById from '../../Pets/PetsById/PetById';

const AdoptionById = () => {
    const {adoptionId, petId, userId} = useParams();
/*     console.log(adoptionId);
    console.log(petId);
    console.log(userId); */
  return (
    <div><PetById petAdoptionId={petId} userAdoptionId={userId} adoptionById={adoptionId}/> </div>
  )
}

export default AdoptionById