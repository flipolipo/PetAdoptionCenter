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
    <div className='sheltersContainer'>
     
        {shelters.map((shelter) => (
          <div className="shelterCard">
            <img
                src={`data:image/jpeg;base64, ${shelter.ImageBase64}`}
                alt=""
                width="250px"
                height="250px"
              />
            <h4 className='textContainer'>{shelter.Name}</h4>
          </div>
        ))}
     
    </div>
  );
};
export default Shelters;
