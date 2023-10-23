import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { address_url } from '../../Service/url';
import './Shelters.css';

const Shelters = () => {
  const [shelters, setShelters] = useState([]);
  const [shelterChosen, setShelterChosen] = useState(false)
  const [shelterData, setShelterData] = useState({})
  const fetchShelters = async () => {
    const response = await axios.get(`${address_url}/Shelters?_limit=3`);
    setShelters(response.data);
    console.log(response.data)
  };

  async function ClickHandler(id) {
    const response = await axios.get(`${address_url}/Shelters/${id}`)
    setShelterChosen(true)
    setShelterData(response.data)
  }
  useEffect(() => {
    fetchShelters();
  }, []);
  return (
    <div>
      {
        shelterChosen ?
          (<div>{shelterData.Id}</div>) :
          (<div className='sheltersContainer'>
            {shelters.map((shelter) => (

              <div className="shelterCard" onClick={() => ClickHandler(shelter.Id)}>
                <img
                  src={`data:image/jpeg;base64, ${shelter.ImageBase64}`}
                  alt=""
                  width="250px"
                  height="250px"
                />
                <h4 className='textContainer'>{shelter.Name}</h4>
              </div>

            ))}
          </div>)
      }
    </div>
  );
};
export default Shelters;
