//Bibliotecas React
import React from 'react';
import ReactDOM from 'react-dom';

//Reac-router-dom
import {
  Route,
  BrowserRouter as Router,
  Navigate,
  Routes
} from 'react-router-dom';

//Css
import './Assets/Css/index.css';

//Serviços
import { parseJwt, usuarioAutenticado } from './Services/auth';


//Components
import Login from './Pages/Login/';
import Perfis from './Pages/Perfis/';
import CadastrarUsuario from './Pages/CadastroUsuario/';
import AtualizarUsuarioPrivilegiado from './Pages/AtualizarUsuarioPrivilegiado/';
import AtualizarUsuarioGeral from './Pages/AtualizarUsuarioGeral/';
import NotFound from './Pages/NotFound/';



import reportWebVitals from './reportWebVitals';



//Permissões

// Children : "filho" (CadastrarConsulta) que está dentro do PermissaoAdm.
const PermissaoLogado = ({ children }) => {
  return( 
    usuarioAutenticado() && parseJwt().role != null ?
     children : <Navigate to="/login" /> 
  );
}



//Rotas
const routing = (
  <Router>
    <div>
      <Routes>
        <Route path='*' element={<NotFound />}/>
        <Route path='/' element={<Login />}/>
        <Route path='/Login' element={<Login />}/>
        <Route path='Perfis' element={<PermissaoLogado><Perfis /></PermissaoLogado>}/> 
        <Route path='CadastrarUsuario' element={<PermissaoLogado><CadastrarUsuario /></PermissaoLogado>}/> 
        <Route path='AtualizarUsuarioPrivilegiado/:userId' element={<PermissaoLogado><AtualizarUsuarioPrivilegiado /></PermissaoLogado>}/> 
        <Route path='AtualizarUsuarioGeral/:userId' element={<PermissaoLogado><AtualizarUsuarioGeral /></PermissaoLogado>}/> 
      </Routes>

    </div>
  </Router>
);

ReactDOM.render(routing, document.getElementById('root'));

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();