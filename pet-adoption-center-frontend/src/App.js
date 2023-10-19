import './App.css';
import Home from './Pages/Home/Home';
import Shelters from './Pages/Shelters/Shelters';
import Adoption from './Pages/Adoption/Adoption';
import TemporaryHouse from './Pages/TemporaryHouse/TemporaryHouse';
import Pets from './Pages/Pets/Pets';
import PetById from './Pages/Pets/PetsById/PetById';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Navbar from './Components/Navbar';
import ScrollHandler from './Components/ScrollHandler';
import Footer from './Components/Footer';

function App() {

  return (
    <Router>
      <div className="App-container">
        <ScrollHandler />
        <Navbar />
        <div className="routes">
          <Routes>
            <Route exact path="/" element={<Home />} />
            <Route path="/Shelters" element={<Shelters />} />
            <Route path="/Shelters/adoptions" element={<Adoption />} />
            <Route
              path="/Shelters/temporaryHouses"
              element={<TemporaryHouse />}
            />
            <Route path="/Users/pets" element={<Pets />} />
            <Route path="/Users/pets/:id" element={<PetById />} />

          </Routes>
        </div>
        <Footer />
      </div>
    </Router>
  );
}

export default App;
