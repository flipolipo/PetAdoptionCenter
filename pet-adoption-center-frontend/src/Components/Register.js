import React, { useState } from 'react';
import axios from 'axios';
import Modal from 'react-modal';
import { address_url } from '../Service/url';

Modal.setAppElement('#root');

const customStyles = {
  content: {
    top: '50%',
    left: '50%',
    right: 'auto',
    bottom: 'auto',
    marginRight: '-50%',
    transform: 'translate(-50%, -50%)',
  },
};

const Register = ({ isLogged }) => {

  const [visible, setVisible] = useState(false);
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [userName, setUserName] = useState('');

  async function registerUser() {
    try {
      const response = await axios.post(`${address_url}/Auth/Register`, {
        Username: userName,
        Email: email,
        Password: password
      });
      if (response.status >= 200 && response.status < 300) {
        console.log('User registered successfully!');
        console.log(response)
        setVisible(false);
      }
    }
    catch (error) {
      console.error(error);
    }
  }

  return (
    <div className='signUpButton'>
      {!isLogged && <button className="buttonSignUp" onClick={() => { setVisible(true); }}>Sign Up</button>}
      <Modal isOpen={visible} onRequestClose={() => setVisible(false)} style={customStyles}>
        <div className="modal-content">
          <input
            className="input-black"
            type="username"
            placeholder="Username"
            value={userName}
            onChange={e => setUserName(e.target.value)}
          />
          <input
            className="input-black"
            type="text"
            placeholder="Email"
            value={email}
            onChange={e => setEmail(e.target.value)}
          />
          <input
            className="input-black"
            type="password"
            placeholder="Password"
            value={password}
            onChange={e => setPassword(e.target.value)}
          />
          <button className="buttonRegister" onClick={registerUser}>Register</button>
          <button className="buttonRegister" onClick={() => setVisible(false)}>Back</button>
        </div>
      </Modal>
    </div>
  );
}

export default Register;