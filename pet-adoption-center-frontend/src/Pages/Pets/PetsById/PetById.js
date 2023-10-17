import React from 'react';
import { useParams } from 'react-router-dom';

const PetById = () => {
  const { id } = useParams();
  return (
    <div>
      <h1>Details for Pet with ID {id}</h1>
    </div>
  );
};

export default PetById;
