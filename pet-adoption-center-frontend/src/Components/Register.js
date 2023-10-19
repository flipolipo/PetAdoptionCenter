
import Model from 'react-modal'
import React, { useState } from 'react';
import axios from 'axios';
import Modal from 'react-modal';

Modal.setAppElement('#root');



const Register = () => {

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


  async function registerUser() {
    try {
      const response = await axios.post('https://localhost:57882/Auth/Register', {
        Username: userName,
        Email: email,
        Password: password
      });
      console.log(response.data);
      // Handle success, e.g. close modal and navigate to user dashboard
    } catch (error) {
      console.error(error);
      // Handle error, e.g. show error message to the user
    }
  }


  const [visible, setVisible] = useState(false);
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [userName, setUserName] = useState('');


  return (
    <div className='signUpButton'>
      <button onClick={() => { setVisible(true); }}>Sign Up</button>
      <Model isOpen={visible} onRequestClose={() => setVisible(false)} style={customStyles}>
        <h1>Register Form Here</h1>

        {/* Fields for the form */}
        <input
          type="username"
          placeholder="Username"
          value={userName}
          onChange={e => setUserName(e.target.value)}
        />

        <input
          type="text"
          placeholder="Email"
          value={email}
          onChange={e => setEmail(e.target.value)}
        />

        <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={e => setPassword(e.target.value)}
        />


        <button onClick={registerUser}>Register</button>


        <button onClick={() => setVisible(false)}>Back</button>
      </Model>
    </div>
  )
}


export default Register