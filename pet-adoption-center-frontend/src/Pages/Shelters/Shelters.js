import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { address_url } from '../../Service/url';
import './Shelters.css';

const Shelters = () => {
  const [shelters, setShelters] = useState([]);
const fetchShelters = async () => {
  const response = await axios.get( 'https://localhost:7292/Shelters?_limit=3');
  setShelters(response.data);
  console.log(response.data)
};
useEffect(() => {
  fetchShelters();
}, []);
  return (
    <div>
      <h2>
        {shelters.map((shelter) => (
          <div classname="shelterContainer">
            
          </div>
        ))}
      </h2>
    </div>
  );
};
export default Shelters;
