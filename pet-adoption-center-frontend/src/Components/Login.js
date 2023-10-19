
import Model from 'react-modal'
import React, { useState } from 'react';
import axios from 'axios';
import Modal from 'react-modal';

Modal.setAppElement('#root');



const Login = () => {

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


  async function loginUser() {
    try {
      const response = await axios.post('https://localhost:57882/Auth/Login', {
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


  return (
    <div className='signInButton'>
      <button onClick={() => { setVisible(true); }}>Sign In</button>
      <Model isOpen={visible} onRequestClose={() => setVisible(false)} style={customStyles}>
        <h1>Login Form Here</h1>

        {/* Fields for the form */}
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


        <button onClick={loginUser}>Login</button>


        <button onClick={() => setVisible(false)}>Back</button>
      </Model>
    </div>
  )
}


export default Login