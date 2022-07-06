import React from 'react';
import { useNavigate } from "react-router-dom";

//Imagens
import Icone_adicionar_usuario from '../../Assets/Images/Adicionar-usuario.svg';
import Icone_usuario from '../../Assets/Images/User-Account.svg';
import Logout from '../../Assets/Images/logout.svg';

//Css
import '../../Assets/Css/index.css';
import { Link } from 'react-router-dom';
import { parseJwt } from '../../Services/auth';


export default function Sidebar() {

    var navigate = useNavigate();

    return (
        <aside className='sidebar'>
            <div className='container_sidebar'>
                <div className='box_foto_perfil_sidebar'>
                    <img className='foto_perfil_sidebar' src={"http://localhost:5000/StaticFiles/UserImages/" + parseJwt().foto} />
                    <span className='nome_perfil_sidebar'>{parseJwt().nome.split(' ')[0]}</span>
                </div>
                <div className="container_nav_sidebar">
                    {parseJwt().role == "1" || parseJwt().role == "2" ?
                        <nav className="nav_sidebar">
                            <Link to={"/CadastrarUsuario"} className="link_sidebar">
                                <div className="box_link_sidebar">
                                    <img className="user_icon_sidebar" src={Icone_adicionar_usuario} alt="2RP Logo" />
                                    <span className="span_box_sidebar">Cadastrar Usuarios</span>
                                </div>
                            </Link>
                            <Link to={"/Perfis"} className="link_sidebar">
                                <div className="box_link_sidebar">
                                    <img className="user_icon_sidebar" src={Icone_usuario} alt="2RP Logo" />
                                    <span className="span_box_sidebar">Ver Usuarios</span>
                                </div>
                            </Link>
                            <Link to={"/"}  onClick={() => localStorage.clear()} className="link_sidebar_logout">
                                <div className="box_link_sidebar">
                                    <img className="user_icon_sidebar" src={Logout} alt="2RP Logo" />
                                    <span className="span_box_sidebar">Sair</span>
                                </div>
                            </Link>
                        </nav>
                        :
                        <nav className="nav_sidebar">
                            <Link to={"/Perfis"} className="link_sidebar">
                                <div className="box_link_sidebar">
                                    <img className="user_icon_sidebar" src={Icone_usuario} alt="2RP Logo" />
                                    <span className="span_box_sidebar">Meu Perfil</span>
                                </div>
                            </Link>
                            <Link to={"/"}  onClick={() => localStorage.clear()} className="link_sidebar_logout">
                                <div className="box_link_sidebar">
                                    <img className="user_icon_sidebar" src={Logout} alt="2RP Logo" />
                                    <span className="span_box_sidebar">Sair</span>
                                </div>
                            </Link>
                        </nav>
                    }
                </div>
                {/* <img className='logo' src={Icone_adicionar_usuario} alt="2RP Logo" />
                    <img className='logo' src={Icone_usuario} alt="2RP Logo" /> */}
            </div>
        </aside>
    );
}