import './App.css';
import Home from './Pages/Home/Home';
import Shelters from './Pages/Shelters/Shelters';
import Adoption from './Pages/Adoption/Adoption';
import TemporaryHouse from './Pages/TemporaryHouse/TemporaryHouse';
import Pets from './Pages/Pets/Pets';
import Profile from './Pages/Profile/Profile';
import PetById from './Pages/Pets/PetsById/PetById';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Navbar from './Components/Navbar';
import ScrollHandler from './Components/ScrollHandler';
import Footer from './Components/Footer';
import React, { useState } from 'react';
import { UserProvider } from './Components/UserContext.js';
import ShelterOwner from './Pages/Profile/ShelterOwner';
import PreadoptionPoll from './Components/PreadoptionPoll.js';
import Meetings from './Components/Meetings.js';
import AdoptionById from './Pages/Adoption/AdoptionById.js/AdoptionById.js';
import ContractAdoption from './Components/ContractAdoption.js';
import ConfirmAdoption from './Components/ConfirmAdoption.js';
import Register from './Components/Register.js';
import PreadoptionPollInfo from './Components/PreadoptionPollInfo.js';
import MeetingsInfo from './Components/MeetingsInfo.js';
import ContractAdoptionInfo from './Components/ContractAdoptionInfo.js';
import PreTempHouseInfo from './Components/TempHouseProccess/PreTempHouseInfo.js';
import PreTemporaryHousePoll from './Components/TempHouseProccess/PreTemporaryHousePoll.js';
import MeetingsTempHouseInfo from './Components/TempHouseProccess/MeetingsTempHouseInfo.js';
import TempHouseById from './Components/TempHouseProccess/TempHouseById.js';
import TempHouseUserPet from './Components/TempHouseProccess/TempHouseUserPet.js';
import ConfirmDeleteTempHouse from './Components/TempHouseProccess/ConfirmDeleteTempHouse.js';
import ShelterById from './Pages/Shelters/ShelterById.js';
import AdoptMeVirtually from './Components/AdoptMeVirtually/AdoptMeVirtually.js';

function App() {
  const [petData, setPetData] = useState([]);
  return (
    <Router>
      <UserProvider>
        <div className="App-container">
          <ScrollHandler />
          <Navbar />
          <div className="routes">
            <Routes>
              <Route exact path="/user/register" element={<Register />} />
              <Route exact path="/" element={<Home />} />
              <Route path="/Shelters" element={<Shelters />} />
              <Route
                path="/Shelters/adoptions"
                element={<Adoption />}
              />
              <Route
                path="/Shelters/adoptions/preadoption-poll"
                element={<PreadoptionPollInfo />}
              />
                <Route
                path="/Shelters/adoptions/meetings-info"
                element={<MeetingsInfo />}
              />
                  <Route
                path="/Shelters/adoptions/contract-adoption-info"
                element={<ContractAdoptionInfo />}
              />
              <Route
                path="/Shelters/adoptions/pets/:id"
                element={<Adoption />}
              />
              <Route
                path="/Shelters/adoptions/pets/:id/users/:userId/preadoption-poll"
                element={<PreadoptionPoll />}
              />
              <Route
                path="/Shelters/adoptions/pets/users/:userId"
                element={<Meetings />}
              />
              <Route
                path="/Shelters/adoptions/:adoptionId/pets/:petId/users/:userId"
                element={<AdoptionById />}
              />
              <Route
                path="/Shelters/adoptions/:adoptionId/pets/:petId/users/:userId/contract-adoption"
                element={<ContractAdoption />}
              />
              <Route
                path="/Shelters/adoptions/:adoptionId/pets/:petId/users/:userId/confirm-adoption"
                element={<ConfirmAdoption />}
              />
              <Route
                path="/Shelters/temporaryHouses"
                element={<TemporaryHouse />}
              />
                <Route
                path="/Shelters/:shelterId/temporaryHouses/pets/:petId"
                element={<TemporaryHouse />}
              /> 
                 <Route
                path="/Shelters/temporaryHouses/pets/users/pre-temporary-house-poll-info"
                element={<PreTempHouseInfo />}
              /> 
                 <Route
                path="/Shelters/:shelterId/temporaryHouses/pets/:petId/users/:userId/pre-temporary-house-poll"
                element={<PreTemporaryHousePoll />}
              /> 
                 <Route
                path="/Shelters/temporaryHouses/pets/users/meetings-temporary-house-poll-info"
                element={<MeetingsTempHouseInfo />}
              /> 
                 <Route
                path="/Shelters/temporaryHouses/:tempHouseId/pets/users/:userId"
                element={<TempHouseById />}
              /> 
                 <Route
                path="/Shelters/temporaryHouses/:tempHouseId/pets/:petId/users/:userId"
                element={<TempHouseUserPet />}
              />
                <Route
                path="/Shelters/temporaryHouses/:tempHouseId/pets/:petId/confirm-delete"
                element={<ConfirmDeleteTempHouse />}
              />
              <Route path="/Users/pets" element={<Pets />} />
              <Route
                path="/Users/pets/:id"
                element={<PetById />}
              />
               <Route
                path="/Shelters/:shelterId/pets/:petId/users/adopt-me-virtually"
                element={<AdoptMeVirtually/>}
              />
              <Route path="/profile" element={<Profile />} />
              <Route
                path="/ShelterOwner/:shelterId"
                element={<ShelterOwner />}
              />
              <Route path="/Shelters/:shelterId" element={<ShelterById />} />
            </Routes>
          </div>

          <Footer />
        </div>
      </UserProvider>
    </Router>
  );
}

export default App;
