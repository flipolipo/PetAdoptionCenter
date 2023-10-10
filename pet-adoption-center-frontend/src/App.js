import './App.css';
import { Home }  from './Components/Home';
import { Shelters } from './Components/Shelters';
import { Adoption } from './Components/Adoption';
import { TemporaryHouse } from './Components/TemporaryHouse';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { NavbarLogo } from './Components/NavbarLogo';
import { NavbarNavigation } from './Components/NavbarNavigation';
function App() {
  return (
    <Router>
    <div className="App-container">
   <NavbarLogo/>
   <NavbarNavigation/>
      <Home/>
   
    </div>
    </Router>
  );
}

export default App;
