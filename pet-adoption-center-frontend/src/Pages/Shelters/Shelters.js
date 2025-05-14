import React, { useCallback, useEffect, useState } from 'react';
import axios from 'axios';
import { address_url } from '../../Service/url';
import './Shelters.css';

import { Link } from 'react-router-dom';
import Fuse from 'fuse.js';


const Shelters = () => {
  const [shelters, setShelters] = useState([]);
  const [shelterChosen, setShelterChosen] = useState(false);
  const [shelterData, setShelterData] = useState([]);
  const [petsAvailable, setPetsAvailable] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [petsPerPage] = useState(4);
  const [currentPets, setCurrentPets] = useState([]);
  const [indexOfFirstPet, setIndexOfFirstPet] = useState(0);
  const [indexOfLastPet, setIndexOfLastPet] = useState(0);
  const [searchBarInput, setSearchBarInput] = useState('');
  const [searchResults, setSearchResults] = useState([]);

  const fetchShelters = async () => {
    const response = await axios.get(`${address_url}/Shelters?_limit=3`);
    setShelters(response.data);
    console.log(response.data);
  };
  // const getPetsFromShelter = async (id) => {
  //   const response = await axios.get(`${address_url}/Shelters/${id}`);
  //   const response2 = await axios.get(
  //     `${address_url}/Shelters/${id}/pets/avaible`
  //   );
  //   setPetsAvailable(response2.data);
  //   console.log(response2.data);
  //   setShelterData(response.data);
  //   setShelterChosen(true);
  // };
  // const ClickHandler = useCallback((id) => {
  //   getPetsFromShelter(id)
  //     .then((response) => {
  //       setShelterData(response.shelter);

  //       setPetsAvailable(response.pets);
  //       setCurrentPets(
  //         petsAvailable.slice(
  //           currentPage * petsPerPage,
  //           indexOfLastPet - petsPerPage
  //         )
  //       );
  //       setCurrentPets(petsAvailable);
  //       setShelterChosen(true);
  //       console.log(response);
  //     })
  //     .then(console.log('pets:', petsAvailable));

  //   setIndexOfLastPet(currentPage * petsPerPage);
  //   setIndexOfFirstPet(indexOfLastPet - petsPerPage);
  //   setCurrentPets(petsAvailable.slice(indexOfFirstPet, indexOfLastPet));
  // });
  const HandleInputChange = (e) => {
    setSearchBarInput(e.target.value);
  };
  function handleSubmit(event) {
    event.preventDefault();
    const fuse = new Fuse(shelters, {
      keys: ['Name', 'ShelterAddress.City', 'ShelterAddress.Street'],
    });
    console.log(shelters);

    const results = fuse.search(searchBarInput);
    console.log(results);
    setSearchResults(results);
  }
  useEffect(() => {
    fetchShelters();
    console.log(`shelters: ${shelters}`)
  }, []);

  return (
    <div>
      <div className="sheltersContainer">
        <form className="searchBar" onSubmit={handleSubmit} action="">
          <input
            type="text"
            placeholder="Search for Name or City"
            name="q"
            onChange={HandleInputChange}
          ></input>
          <button type="submit">
            <img src={process.env.PUBLIC_URL + '/Photo/Search.png'}></img>
          </button>
        </form>
        <div className="shelters">
          {searchResults.length > 0
            ? searchResults.map((shelter, index) => (
              
                <Link to={`/Shelters/${shelter.Id}`}>
                  <div
                    className="shelterCard"
                    // onClick={() => ClickHandler(shelter.item.Id)}
                    key={index}
                  >
                    <img
                      src={`data:image/jpeg;base64, ${shelter.item.ImageBase64}`}
                      alt=""
                      width="250px"
                      height="250px"
                    />
                    <h4 className="textContainer">{shelter.item.Name}</h4>
                  </div>
                </Link>
              ))
            : shelters.map((shelter, index) => (
                <Link to={`/Shelters/${shelter.Id}`}>
                  <div
                    className="shelterCard"
                    // onClick={() => getPetsFromShelter(shelter.Id)}
                    key={index}
                  >
                    <img
                      src={`data:image/jpeg;base64, ${shelter.ImageBase64}`}
                      alt=""
                      width="250px"
                      height="250px"
                    />
                    <h4 className="textContainer">{shelter.Name}</h4>
                  </div>
                </Link>
              ))}
        </div>
      </div>
    </div>
  );
};
export default Shelters;
