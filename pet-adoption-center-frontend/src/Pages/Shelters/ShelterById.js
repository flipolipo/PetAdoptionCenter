import { useParams } from 'react-router-dom';
import FlipCardAvailable from '../../Components/FlipCardAvailable';
import axios from 'axios';
import { address_url } from '../../Service/url';
import { useState, useEffect } from 'react';
import FlipCardShelterPets from '../../Components/FlipCardShelterPets';
import './Shelters.css'
const ShelterById = () => {
  const [shelterData, setShelterData] = useState({});
  const [petsAvailable, setPetsAvailable] = useState([]);
  const { shelterId } = useParams();
  const [shelterOwner, setShelterOwner] = useState({});
  console.log(`ShelterId: ${shelterId}`);
  useEffect(() => {
    const fetchShelterData = async () => {
      console.log(shelterId);
      const response = await axios.get(`${address_url}/Shelters/${shelterId}`);
      setShelterData(response.data);

      const response2 = await axios.get(
        `${address_url}/Shelters/${shelterId}/pets/avaible`
      );
      setPetsAvailable(response2.data);
      console.log(response2);
      console.log(response.data);
    };

    fetchShelterData();
  }, []);
  useEffect(() => {
    setShelterOwner(GetShelterOwner);
  }, [shelterData]);
  if (shelterData.Adoptions) {
    console.log(`length: ${shelterData.Adoptions}`);
    console.log(shelterData);
    console.log(shelterData.Adoptions.length);
  }
  console.log(petsAvailable);

  const GetShelterOwner = () => {
    let shelterBoss = {}
    if (shelterData.ShelterUsers) {
       shelterBoss = shelterData.ShelterUsers.find((shelterUser) =>
        shelterUser.Roles.some((role) => role.Title === 0)
      );
    }
    console.log(shelterBoss);
    return shelterBoss;
  };
  return (
    <div className="chosenShelterContainer">
      <div className="shelterPhotoAndContactInfo">
        <div className="photoContainer">
          <img
            className="photo"
            src={`data:image/jpeg;base64, ${shelterData.ImageBase64}`}
            alt={shelterData.Name}
          ></img>
          <h4>{shelterData.Name}</h4>
        </div>
        <div className="contactInfoContainer">
          <h1>Contact Us!</h1>
          {shelterData &&
            shelterData.ShelterAddress &&
            shelterData.Adoptions && shelterOwner?.BasicInformation && shelterData.ShelterPets &&(
              <>
                <h3>{shelterData.ShelterAddress.City} </h3>
                <h3>
                  {shelterData.ShelterAddress.Street}{' '}
                  {shelterData.ShelterAddress.HouseNumber}/
                  {shelterData.ShelterAddress.FlatNumber}
                </h3>
                <h3 className='shelterInfo'>Phone Number:{shelterData.PhoneNumber}</h3>
                <h3 className='shelterInfo'>
                  Proccessed Adoptions: {shelterData.Adoptions.length}
                </h3>
                <h3 className='shelterInfo'>
                  Shelter Owner: {shelterOwner.BasicInformation.Name}{' '}
                  {shelterOwner.BasicInformation.Surname}
                </h3>
                <h3 className='shelterInfo'>Saved Pets: {shelterData.ShelterPets.length}</h3>
              </>
            )}
        </div>
      </div>
      <div className="shelterDescription">{shelterData.ShelterDescription}</div>
      <div>
        <div className="card-container">
          <FlipCardShelterPets id={shelterId} />
        </div>
      </div>
    </div>
  );
};

export default ShelterById;
