//Hooks
import { useState } from 'react';
import { useNavigate } from "react-router-dom";

//Bibliotecas React
import axios from 'axios';

//Serviços
import api from '../../Services/api';


//Components
import Header from '../../Components/Header/';
import Foooter from '../../Components/Footer/';


//Css
import "../../Assets/Css/index.css";

export default function Login() {

    //States
    const [email, setEmail] = useState('');
    const [senha, setSenha] = useState('');
    const [validacao, setValidacao] = useState(false);
    var navigate = useNavigate();

    function FazerLogin(event) {

        setValidacao(true)
        event.preventDefault();


        //chamando api
        api.post('/Login', {
            email: email,
            senha: senha
        })
            .then((resposta) => {
                if (resposta.status === 200) {

                    //adicionando token no localStorage do navegador
                    localStorage.setItem('usuario-login', resposta.data.token);
                    setValidacao(false);
                    navigate("/Perfis")
                }
            }
            )
            .catch(erro => console.log(erro))
    }

    return (
        <div>
            <Header />
            {/* <!-- Main --> */}
            <main>
                <div className="container_main_login container">
                    <div className="box_form_login">
                        <div className="box_span_login">
                            <span className="box_span_titulo_login">Bem-vindo!</span>
                            <span className="box_span_subtitulo">Coloque seu email e sua senha para continuar</span>
                        </div>

                        <form onSubmit={FazerLogin} action="" className="form_login">
                            <label></label>
                            <input type="text"
                                onChange={(event) => setEmail(event.target.value)}
                                placeholder="Email" value={email} className="input_login"
                            />
                            <label></label>
                            <input type="text" placeholder="Senha" value={senha}
                                onChange={(event) => setSenha(event.target.value)}
                                className="input_login" />
                            <button type="submit" className="button_login">Entrar</button>
                        </form>
                    </div>
                </div>
            </main>
            <Foooter />
        </div>
    );
}