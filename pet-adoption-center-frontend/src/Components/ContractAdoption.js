import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { address_url } from '../Service/url';
import { fetchDataForPet } from '../Service/fetchDataForPet';
import GenderPetLabel from '../Components/Enum/GenderPetLabel';
import TypePetLabel from '../Components/Enum/TypePetLabel';
import SizePetLabel from './Enum/SizePetLabel';
import { Link, useParams } from 'react-router-dom';
import './ContractAdoption.css'

const ContractAdoption = () => {
  const { adoptionId, petId, userId } = useParams();
  console.log(petId);
  console.log(userId);
  console.log(adoptionId);

  const [formData, setFormData] = useState({
    adopterName: '',
    adopterSurname: '',
    pesel: '',
    cityName: '',
    streetName: '',
    houseNumber: '',
  });

  const [formErrors, setFormErrors] = useState({});
  const [contractAdoptionData, setContractAdoptionData] = useState('');
  const [petAdoptionData, setPetAdoptionData] = useState({});
  const [shelterAdoptionData, setShelterAdoptionData] = useState({});
  const [ownerName, setOwnerName] = useState({ name: '', surname: '' });
  const [submissionSuccess, setSubmissionSuccess] = useState(false);
  const [contractAdoptionVisible, setContractAdoptionVisible] = useState(false);
  const [userData, setUserData] = useState({});

  useEffect(() => {
    const fetchData = async () => {
      try {
        const petData = await fetchDataForPet(petId);
        setPetAdoptionData(petData);
        setSubmissionSuccess(false);
        setContractAdoptionVisible(true);
        console.log(petData);

        const shelterData = await fetchDataForShelter(petData.ShelterId);
        setShelterAdoptionData(shelterData);
        setSubmissionSuccess(false);
        setContractAdoptionVisible(true);

        const owner = await shelterOwner(shelterData);
        setOwnerName(owner);
        setSubmissionSuccess(false);
        setContractAdoptionVisible(true);
      } catch (error) {
        console.error('Pet download error:', error);
      }
    };

    fetchData();
  }, [petId]);

  useEffect(() => {
    const fetchProfileData = async () => {
      try {
        const response = await axios.get(`${address_url}/Users/${userId}`, {
          headers: {
            Authorization: `Bearer ${userId.token}`,
          },
        });
        setUserData(response.data);
        setSubmissionSuccess(false);
        setContractAdoptionVisible(true);
        console.log(response.data.BasicInformation.Name);
        console.log(response.data.BasicInformation.Surname);

      } catch (err) {
        console.log(err);
      }
    };

    fetchProfileData();
  }, [userId, userId.token]);

  const fetchDataForShelter = async (shelterId) => {
    try {
      const response = await axios.get(`${address_url}/Shelters/${shelterId}`);
      const shelterData = response.data;
      setShelterAdoptionData(shelterData);
      console.log(shelterData);
      return shelterData;
    } catch (error) {
      console.error(error);
    }
  };

  const shelterOwner = async (shelterAdoptionData) => {
    let shelterOwnerName = '';
    let shelterOwnerSurname = '';

    if (shelterAdoptionData && shelterAdoptionData.ShelterUsers) {
      shelterAdoptionData.ShelterUsers.forEach((user) => {
        if (user.Roles && user.Roles.length > 0) {
          user.Roles.forEach((role) => {
            if (role.Title === 0) {
              if (user.BasicInformation) {
                shelterOwnerName = user.BasicInformation.Name;
                shelterOwnerSurname = user.BasicInformation.Surname;
              }
            }
          });
        }
      });
    }

    //console.log('shelterOwnerName:', shelterOwnerName);
    //console.log('shelterOwnerSurname:', shelterOwnerSurname);

    return {
      name: shelterOwnerName,
      surname: shelterOwnerSurname,
    };
  };

  const vaccinations = async (petAdoptionData) => {
    let vaccinationsName = [];
    if (
      petAdoptionData &&
      petAdoptionData.BasicHealthInfo.Vaccinations.length > 0
    ) {
      petAdoptionData.BasicHealthInfo.Vaccinations.forEach((vaccine) => {
        if (vaccine.VaccinationName) {
          vaccinationsName.push(vaccine.VaccinationName);
        }
      });
    }
    console.log('Vaccinations:', vaccinationsName);
    return vaccinationsName;
  };

  const diseases = async (petAdoptionData) => {
    let diseasesName = [];
    if (
      petAdoptionData &&
      petAdoptionData.BasicHealthInfo.MedicalHistory.length > 0
    ) {
      petAdoptionData.BasicHealthInfo.MedicalHistory.forEach((disease) => {
        if (disease.NameOfdisease) {
          diseasesName.push(disease.NameOfdisease);
        }
      });
    }
    console.log('Diseases:', diseasesName);
    return diseasesName;
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
    setFormErrors({
      ...formErrors,
      [name]: '',
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    const errors = {};
    if (formData.adopterName !== userData.BasicInformation.Name) {
      errors.adopterName = 'Wrong name to submit this form.';
    }
    if (formData.adopterSurname !== userData.BasicInformation.Surname) {
      errors.adopterSurname = 'Wrong surname to submit this form.';
    }
    for (const key in formData) {
      if (!formData[key]) {
        errors[key] = 'This field is required.';
      }
    }
    if (Object.keys(errors).length > 0) {
      setFormErrors(errors);
    } else {
      const vaccinationNames = await vaccinations(petAdoptionData);
      const vaccinationsString = vaccinationNames.join(', ');
      const diseasesName = await diseases(petAdoptionData);
      const diseasesNameString = diseasesName.join(', ');
      const contractString = `ADOPTION AGREEMENT FOR A PET No. ${adoptionId} Made on the ${day} day of ${month} ${year} between:
      Shelter ${shelterAdoptionData.Name}
      represented by ${ownerName.name} ${ownerName.surname}, acting as an
      authorized person based on the relevant register data, and Mr./Mrs. (name): ${
        formData.adopterName
      } (surname): ${formData.adopterSurname}, PESEL ${formData.pesel}, residing in 
      ${formData.cityName} at ${formData.streetName} no ${
        formData.houseNumber
      }, hereinafter referred
      to as the Adopter, hereinafter collectively referred to as the
      Parties. 
      § 1
      The subject of this agreement is the adoption of a pet.
      Name: ${petAdoptionData.BasicHealthInfo.Name},
      Gender: ${GenderPetLabel(petAdoptionData.Gender)},
      Type: ${TypePetLabel(petAdoptionData.Type)},
      Age: ${petAdoptionData.BasicHealthInfo.Age},
      Size: ${SizePetLabel(petAdoptionData.BasicHealthInfo.Size)},
      Is neutred: ${petAdoptionData.BasicHealthInfo.IsNeutered ? 'Yes' : 'No'}, 
      Vaccinations name: ${vaccinationsString},
      Name of disease: ${diseasesNameString}.
      § 2
      The Shelter transfers ownership of the animal to the Adopter,
      and the Adopter declares their consent to this. The Parties
      declare that the transfer of the animal occurs simultaneously
      with the signing of this agreement.
      § 3
      The Shelter declares, to the best of its knowledge, that the
      animal being given for adoption is a homeless animal. The
      Shelter also declares, to the best of its knowledge, that the
      adopted animal has not exhibited any symptoms of illness
      before the adoption, or any previously observed symptoms have
      subsided. The Adopter declares that they have reviewed the
      physical and health condition of the animal and have no
      objections. The Shelter is not responsible for injuries or
      illnesses that occur or are discovered after adoption.
      § 4
      The Adopter undertakes to:
      Register the animal with the relevant authorities.
Provide the animal with proper veterinary care, including mandatory rabies vaccinations.
Ensure the animal receives food, water, and shelter.
Keep the animal within their property boundaries and not release it unsupervised in unauthorized areas.
Show respect, protection, and care to the animal with due regard, not to sell, give away, or exchange it.
Not chain the animal or confine it to a cage without the opportunity for walks to enable the animal to engage in social behaviors.
Return the animal to the Shelter within 7 days if it does not meet the Adopter's expectations.
Not subject the animal to medical experiments or any other experiments.
Notify the Shelter within 24 hours if the animal goes missing.
Have the specified veterinarian perform the sterilization procedure at the Shelter's expense:
Within 3 months from the date of signing the adoption agreement, in case the animal has not been sterilized before adoption.
For female pets after their first heat cycle when they are too young for sterilization at the time of adoption. Until mandatory sterilization is performed, the Adopter will prevent the female from breeding.
The Shelter reserves the right to inspect the animal's living conditions, and the Adopter agrees to this.
§ 5
The Shelter declares that it has the right to revoke this
donation agreement if the Adopter engages in gross ingratitude
(art. 898 of the Civil Code), which the Adopter acknowledges.
The Parties understand that gross ingratitude includes, in
particular:
Failure to provide the animal with appropriate food, access to water, and shelter according to its species-specific and physiological needs.
Neglecting to supervise and provide constant and effective care for the animal.
Abandoning the animal.
Failing to provide veterinary care to the animal, especially not administering mandatory vaccinations for pets.
Subjecting the animal to medical experiments or any other experiments.
Violation by the Adopter of art. 6 para. 1a in connection with para. 2 of the Act of August 21, 1997 on the protection of animals.
Permanent or long-term transfer of the animal to a third party in violation of § 2 para. 2 of this agreement.
Failure to fulfill § 3 para. 4 of this agreement.
Upon the revocation of the donation, the Adopter is obliged to immediately return the animal upon the Shelter's request.
§ 6
Disputes arising from this agreement will be resolved by the
court with jurisdiction over the Shelter's headquarters.
This agreement has been drawn up in two identical copies, one
    for each party.
    Attachment - Adoption card is an integral part of the
    agreement.
    SIGNATURES OF THE PARTIES
    .......................... (Adopter's signature)
    .......................... (Seal and signature of Shelter
      employee)`;
      console.log(contractString);

      try {
        const response = await axios.post(
          `${address_url}/Shelters/${
            petAdoptionData.ShelterId
          }/pets/${petId}/users/${userId}/adoptions/${adoptionId}/contract-adoption?contractAdoption=${encodeURIComponent(
            contractString
          )}`
        );
        setContractAdoptionData(response.data);
        setSubmissionSuccess(true);
        setContractAdoptionVisible(false);
      } catch (error) {}
    }
  };
  const currentDate = new Date();
  const day = currentDate.getDate();
  const month = currentDate.toLocaleString('default', { month: 'long' });
  const year = currentDate.getFullYear();

  return (
    <div className="contract-adoption-to-sign">
      {submissionSuccess ? (
        <>
          {' '}
          <div className="contract-adoption-to-sign-info">
            <h2 className="contract-adoption-to-sign-h2">
              The adoption contract has been successfully sent. Feel free to
              visit the shelter in person to sign the contract and pick up your
              new friend.{' '}
            </h2>
            <h2 className="contract-adoption-to-sign-h2">
              {' '}
              Return to the main page.
            </h2>
          </div>
          <div className="contract-adoption-to-sign-link">
            <Link
              to={`/Shelters/adoptions/pets/users/${userId}`}
              className="contract-adoption-to-sign-button"
            >
              GO BACK
            </Link>
          </div>
        </>
      ) : (
        <>
          {contractAdoptionVisible && (
            <form className='contract-adopion-form' onSubmit={handleSubmit}>
              <label>ADOPTION AGREEMENT FOR A PET No. {adoptionId}</label>
              <label>
                {' '}
                Made on the {day} day of {month} {year} between:{' '}
              </label>
              {shelterAdoptionData && ownerName && (
                <label>
                  {' '}
                  Shelter {shelterAdoptionData.Name}
                  represented by {ownerName.name} {ownerName.surname}, acting as
                  an authorized person based on the relevant register data,{' '}
                </label>
              )}

              <label>and Mr./Mrs. (name)</label>
              <input
                type="text"
                name="adopterName"
                value={formData.adopterName}
                onChange={handleChange}
                className="form-contract-adoption"
              />
              {formErrors.adopterName && (
                <p className="error-message">{formErrors.adopterName}</p>
              )}
              <label>(surname): </label>
              <input
                type="text"
                name="adopterSurname"
                value={formData.adopterSurname}
                onChange={handleChange}
                className="form-contract-adoption"
              />
              {formErrors.adopterSurname && (
                <p className="error-message">{formErrors.adopterSurname}</p>
              )}
              <label>PESEL</label>
              <input
                type="text"
                name="pesel"
                value={formData.pesel}
                onChange={handleChange}
                className="form-contract-adoption"
                pattern="[0-9]{11}"
                title="PESEL must be 11 digits"
              />
              {formErrors.pesel && (
                <p className="error-message">{formErrors.pesel}</p>
              )}
              <label>residing in</label>
              <input
                type="text"
                name="cityName"
                value={formData.cityName}
                onChange={handleChange}
                className="form-contract-adoption"
              />
              {formErrors.cityName && (
                <p className="error-message">{formErrors.cityName}</p>
              )}
              <label>at</label>
              <input
                type="text"
                name="streetName"
                value={formData.streetName}
                onChange={handleChange}
                className="form-contract-adoption"
              />
              {formErrors.streetName && (
                <p className="error-message">{formErrors.streetName}</p>
              )}
              <label>no</label>
              <input
                type="text"
                name="houseNumber"
                value={formData.houseNumber}
                onChange={handleChange}
                className="form-contract-adoption"
              />
              {formErrors.houseNumber && (
                <p className="error-message">{formErrors.houseNumber}</p>
              )}
              <label>
                hereinafter referred to as the Adopter, hereinafter collectively
                referred to as the Parties.
              </label>
              <label>§ 1</label>
              <label>
                The subject of this agreement is the adoption of a pet.
              </label>
              {petAdoptionData && petAdoptionData.BasicHealthInfo && (
                <label>Name: {petAdoptionData.BasicHealthInfo.Name}</label>
              )}
              <label>Gender: {GenderPetLabel(petAdoptionData.Gender)}</label>
              <label>Type: {TypePetLabel(petAdoptionData.Type)}</label>
              {petAdoptionData && petAdoptionData.BasicHealthInfo && (
                <>
                  <label>Age: {petAdoptionData.BasicHealthInfo.Age}</label>
                  <label>
                    Size: {SizePetLabel(petAdoptionData.BasicHealthInfo.Size)}
                  </label>
                  <label>
                    Is neutred:{' '}
                    {petAdoptionData.BasicHealthInfo.IsNeutered ? 'Yes' : 'No'}
                  </label>
                </>
              )}
              <label>Vaccinations name:</label>
              <ul>
                {petAdoptionData &&
                petAdoptionData.BasicHealthInfo &&
                petAdoptionData.BasicHealthInfo.Vaccinations &&
                petAdoptionData.BasicHealthInfo.Vaccinations.length > 0 ? (
                  petAdoptionData.BasicHealthInfo.Vaccinations.map(
                    (vaccine, index) => (
                      <li key={index}>{vaccine.VaccinationName}</li>
                    )
                  )
                ) : (
                  <li>No vaccinations </li>
                )}
              </ul>
              <label>Name of disease:</label>
              <ul>
                {petAdoptionData &&
                petAdoptionData.BasicHealthInfo &&
                petAdoptionData.BasicHealthInfo.MedicalHistory &&
                petAdoptionData.BasicHealthInfo.MedicalHistory.length > 0 ? (
                  petAdoptionData.BasicHealthInfo.MedicalHistory.map(
                    (disease, index) => (
                      <li key={index}>{disease.NameOfdisease}</li>
                    )
                  )
                ) : (
                  <li>No disease</li>
                )}
              </ul>
              <label> § 2</label>
              <label>
                The Shelter transfers ownership of the animal to the Adopter,
                and the Adopter declares their consent to this. The Parties
                declare that the transfer of the animal occurs simultaneously
                with the signing of this agreement.
              </label>
              <label>§ 3</label>
              <label>
                {' '}
                The Shelter declares, to the best of its knowledge, that the
                animal being given for adoption is a homeless animal. The
                Shelter also declares, to the best of its knowledge, that the
                adopted animal has not exhibited any symptoms of illness before
                the adoption, or any previously observed symptoms have subsided.
                The Adopter declares that they have reviewed the physical and
                health condition of the animal and have no objections. The
                Shelter is not responsible for injuries or illnesses that occur
                or are discovered after adoption.
              </label>
              <label>§ 4</label>
              <label>The Adopter undertakes to:</label>
              <ul>
                <li>Register the animal with the relevant authorities.</li>
                <li>
                  Provide the animal with proper veterinary care, including
                  mandatory rabies vaccinations.
                </li>
                <li>Ensure the animal receives food, water, and shelter.</li>
                <li>
                  Keep the animal within their property boundaries and not
                  release it unsupervised in unauthorized areas.
                </li>
                <li>
                  Show respect, protection, and care to the animal with due
                  regard, not to sell, give away, or exchange it.
                </li>
                <li>
                  Not chain the animal or confine it to a cage without the
                  opportunity for walks to enable the animal to engage in social
                  behaviors.
                </li>
                <li>
                  Return the animal to the Shelter within 7 days if it does not
                  meet the Adopter's expectations.
                </li>
                <li>
                  Not subject the animal to medical experiments or any other
                  experiments.
                </li>
                <li>
                  Notify the Shelter within 24 hours if the animal goes missing.
                </li>
                <li>
                  Have the specified veterinarian perform the sterilization
                  procedure at the Shelter's expense within 3 months from the
                  date of signing the adoption agreement, in case the animal has
                  not been sterilized before adoption.
                </li>
                <li>
                  For female pets after their first heat cycle when they are too
                  young for sterilization at the time of adoption. Until
                  mandatory sterilization is performed, the Adopter will prevent
                  the female from breeding.
                </li>
                <li>
                  The Shelter reserves the right to inspect the animal's living
                  conditions, and the Adopter agrees to this.
                </li>
              </ul>
              <label>§ 5</label>
              <label>
                {' '}
                The Shelter declares that it has the right to revoke this
                donation agreement if the Adopter engages in gross ingratitude
                (art. 898 of the Civil Code), which the Adopter acknowledges.
                The Parties understand that gross ingratitude includes, in
                particular:
              </label>
              <ul>
                <li>
                  Failure to provide the animal with appropriate food, access to
                  water, and shelter according to its species-specific and
                  physiological needs.
                </li>
                <li>
                  Neglecting to supervise and provide constant and effective
                  care for the animal.
                </li>
                <li>Abandoning the animal.</li>
                <li>
                  Failing to provide veterinary care to the animal, especially
                  not administering mandatory vaccinations for pets.
                </li>
                <li>
                  Subjecting the animal to medical experiments or any other
                  experiments.
                </li>
                <li>
                  Violation by the Adopter of art. 6 para. 1a in connection with
                  para. 2 of the Act of August 21, 1997 on the protection of
                  animals.
                </li>
                <li>
                  Permanent or long-term transfer of the animal to a third party
                  in violation of § 2 para. 2 of this agreement.
                </li>
                <li>Failure to fulfill § 3 para. 4 of this agreement.</li>
                <li>
                  Upon the revocation of the donation, the Adopter is obliged to
                  immediately return the animal upon the Shelter's request.
                </li>
              </ul>

              <label>§ 6</label>
              <label>
                {' '}
                Disputes arising from this agreement will be resolved by the
                court with jurisdiction over the Shelter's headquarters.
              </label>
              <label>
                This agreement has been drawn up in two identical copies, one
                for each party.
              </label>
              <label>
                Attachment - Adoption card is an integral part of the agreement.
              </label>
              <label>SIGNATURES OF THE PARTIES</label>
              <label>.......................... (Adopter's signature)</label>
              <label>
                {' '}
                .......................... (Seal and signature of Shelter
                employee)
              </label>
              <button className='contract-adoption-to-sign-button' type="submit">Submit</button>
            </form>
          )}
        </>
      )}
    </div>
  );
};

export default ContractAdoption;
