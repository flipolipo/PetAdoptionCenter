import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useUser } from '../../Components/UserContext';
import { address_url } from '../../Service/url';
const Pets = () => {
  const { user } = useUser();
  const [userPets, setUserPets] = useState([]);

  useEffect(() => {
    const fetchUserPets = async () => {
      try {
        if (user && user.id) {
          const response = await axios.get(`${address_url}/Users/${user.id}/pets`, {
            headers: {
              'Authorization': `Bearer ${user.token}`
            }
          });
          setUserPets(response.data);
          console.log(response.data);
        }
      } catch (error) {
        console.error('Error fetching user pets:', error);
      }
    };

    fetchUserPets();
  }, [user.id, user.token]);

  return (
    <div>
      <h2>Welcome, {user.username}!</h2>
      <h3>Your Pets:</h3>
      <ul>
        {userPets.map((pet) => (
          <li key={pet.Id}>{pet.Id}</li>
        ))}
      </ul>
    </div>
  );
};

export default Pets;
