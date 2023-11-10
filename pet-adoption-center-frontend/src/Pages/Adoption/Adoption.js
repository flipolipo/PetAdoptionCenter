import React, { useEffect, useState } from 'react';
import './Adoption.css';
import FlipCardAdopted from '../../Components/FlipCardAdopted';
import FlipCardAvailable from '../../Components/FlipCardAvailable';
import { Link, useParams } from 'react-router-dom';
import { useUser } from '../../Components/UserContext';
import axios from 'axios';
import { address_url } from '../../Service/url';

const Adoption = () => {
  const { id } = useParams();
  //console.log(id);
  const { user, setUser } = useUser();
  const [userData, setUserData] = useState([]);

  useEffect(() => {
    const fetchProfileData = async () => {
      try {
        const response = await axios.get(`${address_url}/Users/${user.id}`, {
          headers: {
            Authorization: `Bearer ${user.token}`,
          },
        });
        setUserData(response.data);
        // console.log(response.data.Adoptions);
      } catch (err) {
        console.log(err);
      }
    };

    fetchProfileData();
  }, [user.id, user.token]);

  return (
    <div className="adoption-container">
      <div className="adoption-button-card">
        <div className="button-more-info">
        <Link to={`/Users/pets`} className="find-pet">
            Find your new best friend
          </Link>
          {user.id && id ? (
            <Link
              to={`/Shelters/adoptions/pets/${id}/users/${user.id}`}
              className="find-pet"
            >
              Preadoption Poll
            </Link>
          ) : (
            <div className="button-more-info">
              <Link
                to={`/Shelters/adoptions/preadoption-poll`}
                className="find-pet"
              >
                Preadoption Poll
              </Link>
            </div>
          )}
          {user.id ? (
            <Link
              to={`/Shelters/adoptions/pets/users/${user.id}`}
              className="find-pet"
            >
              Meetings to know your pet
            </Link>
          ) : (
            <div className="button-more-info">
              <Link
                to={`/Shelters/adoptions/meetings-info`}
                className="find-pet"
              >
                Meetings to know your pet
              </Link>
            </div>
          )}
          {user.id ? (
            <Link
              to={`/Shelters/adoptions/pets/users/${user.id}`}
              className="find-pet"
            >
              Contract adoption
            </Link>
          ) : (
            <div className="button-more-info">
              <Link
                to={`/Shelters/adoptions/contract-adoption-info`}
                className="find-pet"
              >
                Contract adoption
              </Link>
            </div>
          )}
        
        </div>
        <div className="adoption-card-page-adoption">
          <div className="adoption-process-info">
            <h2 className="title-pet">ADOPTION PROCESS INFO</h2>
            <ul className='adoption-process'>
              <li>
                Sign up on our website and complete the basic information on
                your profile.
              </li>
              <li>Log into your account.</li>
              <li>Select a pet from the available options.</li>
              <li>Complete the pre-adoption poll for the chosen pet.</li>
              <li>Choose a meeting from the calendar named "Know me."</li>
              <li>
                After the meeting, you will be asked to confirm the adoption. If
                you change your mind, you can choose to delete the adoption.
              </li>
              <li>Fill out the adoption contract.</li>
              <li>
                You will receive a message that you can come to us to submit
                your personal signature (remember to bring your document) and
                pick up your new friend.
              </li>
            </ul>
            <h3>Feel free to click on the bottom to have more information</h3>
          </div>
        </div>
      </div>
      <div className="petsAvailableForAdoption">
        <div className="pet-inscription">
          <h2 className="title-pet">Pets available for adoption</h2>
        </div>
        <div className="pet-card">
          <FlipCardAvailable />
        </div>
      </div>
    </div>
  );
};

export default Adoption;
