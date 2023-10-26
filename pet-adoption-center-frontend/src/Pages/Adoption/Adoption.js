import React, { useEffect, useState } from 'react';
import './Adoption.css';
import FlipCardAdopted from '../../Components/FlipCardAdopted';
import FlipCardAvailable from '../../Components/FlipCardAvailable';
import { Link } from 'react-router-dom';
import { useUser } from '../../Components/UserContext';
import axios from 'axios';
import { address_url } from '../../Service/url';


const Adoption = ({petData, setPetData}) => {
  const { user, setUser } = useUser();
  const [userData, setUserData] = useState([]);
  const [preadoptionPollVisible, setPreadoptionPollVisible] = useState(false);
  const [meetingsVisible, setMeettingsVisible] = useState(false);
  const [contractAdoptionVisible, setContractAdoptionVisible] = useState(false);

  useEffect(() => {
    const fetchProfileData = async () => {
        try {
            const response = await axios.get(`${address_url}/Users/${user.id}`, {
                headers: {
                    'Authorization': `Bearer ${user.token}`
                }
            });
            setUserData(response.data);
            console.log(response.data);

        } catch (err) {
           console.log(err);
        } 
    };

    fetchProfileData();
}, [user.id, user.token]);

console.log(user);
console.log(petData);

  const showPreadoptionPoll = () => {
    setPreadoptionPollVisible(true);
  };

  const hidePreadoptionPoll = () => {
    setPreadoptionPollVisible(false);
  };
  const showInfoMeetings = () => {
    setMeettingsVisible(true);
  };

  const hideInfoMeetings = () => {
    setMeettingsVisible(false);
  };
  const showContractAdoption = () => {
    setContractAdoptionVisible(true);
  };

  const hideContractAdoption = () => {
    setContractAdoptionVisible(false);
  };

  return (
    <div>
    UserId =  {user.id}
      <div className="adoption-container">
        {preadoptionPollVisible ? ( 
          <div className="preadoption-poll">
            <h2>SPRAWDZAM CZY TO DZIALA (preadoption poll)</h2>
            <button onClick={hidePreadoptionPoll}>Close Preadoption Poll</button>
            <h2>Jesli chcesz zaadoptowac to prosze sie zalogowac lub zarejestrowac</h2>
          </div>
        ) : meetingsVisible ? ( <div className="meetings">
        <h2>SPRAWDZAM CZY TO DZIALA (Meetings)</h2>
        <button onClick={hideInfoMeetings}>Close Info Meetings</button>
      </div>) : contractAdoptionVisible ? ( <div className="contract-adoption">
        <h2>SPRAWDZAM CZY TO DZIALA (Contract)</h2>
        <button onClick={hideContractAdoption}>Close Contract Adoption</button>
      </div>): (
          <>
            <div className="adoption-button-card">
              <div className="button-more-info">
                <button className='button-adoption' onClick={showPreadoptionPoll}>Preadoption Poll</button>
                <button className='button-adoption' onClick={showInfoMeetings}>Meetings to know your pet</button>
                <button className='button-adoption' onClick={showContractAdoption}>Contract adoption</button>
                <Link to={`/Users/pets`} className="find-pet">
                  Find your new best friend
                </Link>
              </div>
              <div className="adoption-card-page-adoption">
                <div className="pet-inscription">
                  <h2 className='title-pet'>Adopted pets</h2>
                </div>
                <FlipCardAdopted />
              </div>
            </div>
            <div className="petsAvailableForAdoption">
              <div className="pet-inscription">
                <h2 className='title-pet'>Pets available for adoption</h2>
              </div>
              <div className="pet-card">
                <FlipCardAvailable />
              </div>
            </div>
          </>
        )}
      </div>
    </div>
  );
};

export default Adoption;