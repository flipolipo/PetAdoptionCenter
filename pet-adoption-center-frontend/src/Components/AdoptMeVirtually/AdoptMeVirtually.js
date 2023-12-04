import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { fetchDataForPet } from '../../Service/fetchDataForPet';
import { fetchDataForShelter } from '../../Service/fetchDataForShelter';
import './AdoptMeVirtually.css';

const AdoptMeVirtually = () => {
  const { shelterId, petId } = useParams();
  console.log(shelterId);
  console.log(petId);
  const [petSelectedData, setPetSelectedData] = useState({});
  const [shelterData, setShelterData] = useState({});

  useEffect(() => {
    const fetchPetData = async () => {
      try {
        const responseData = await fetchDataForPet(petId);
        setPetSelectedData(responseData);
      } catch (err) {
        console.log('shelter fetch error: ' + err);
      }
    };
    fetchPetData();
  }, [petId]);

  useEffect(() => {
    const fetchShelterData = async () => {
      try {
        const responseData = await fetchDataForShelter(shelterId);
        setShelterData(responseData);
      } catch (err) {
        console.log('shelter fetch error: ' + err);
      }
    };
    fetchShelterData();
  }, [shelterId]);

  return (
    <div className="virtual-adoption-contener">
      <img
        className="pet-img"
        src={`data:image/jpeg;base64, ${petSelectedData.ImageBase64}`}
        alt=""
      />
      <h2 className="virtual-adoption">
        If you would like to help, please make a donation to the shelter's
        account.
      </h2>
      <h2>Here is the account number:</h2>
      <h2 className="important"> {shelterData.BankNumber}</h2>
      <h2>Thank you for every penny.</h2>
      <h1 className="pet-name">
        {' '}
        <img
          src={process.env.PUBLIC_URL + '/Photo/redPaw.png'}
          alt="Lapka"
          className="lapka"
        />
        {petSelectedData.BasicHealthInfo?.Name}{' '}
        <img
          src={process.env.PUBLIC_URL + '/Photo/redPaw.png'}
          alt="Lapka"
          className="lapka"
        />
      </h1>
    </div>
  );
};

export default AdoptMeVirtually;
