import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useUser } from '../../../Components/UserContext';
import { address_url } from '../../../Service/url';

function ShelterFilter({ onChange }) {
  const { user } = useUser();
  const [shelters, setShelters] = useState([]);
  const [error, setError] = useState(null);
  const [selectedShelter, setSelectedShelter] = useState('');

  useEffect(() => {
    const fetchShelters = async () => {
      try {
        const response = await axios.get(`${address_url}/Shelters`, {
          headers: {
            Authorization: `Bearer ${user.token}`,
          },
        });
        setShelters(response.data);
      } catch (err) {
        setError(err.message);
      }
    };
    fetchShelters();
  }, [user.id, user.token]);

  const handleShelterChange = (event) => {
    const shelterId = event.target.value;
    setSelectedShelter(shelterId);
    onChange(shelterId);
  };

  return (
    <div className="filter">
      <h3>Filter by Shelter</h3>
      <select value={selectedShelter} onChange={handleShelterChange}>
        <option value="">Select a shelter</option>
        {shelters ? (
          shelters.map((shelter) => (
            <option key={shelter.Id} value={shelter.Id}>
              {shelter.Name}
            </option>
          ))
        ) : null}
      </select>
    </div>
  );
}

export default ShelterFilter;

