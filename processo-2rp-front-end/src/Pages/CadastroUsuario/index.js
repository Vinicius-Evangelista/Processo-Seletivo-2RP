//Hooks
import { useEffect, useState } from 'react';
import { useNavigate } from "react-router-dom";
import toast, { Toaster } from 'react-hot-toast';

//Bibliotecas React
import axios from 'axios';

//Serviços
import api from '../../Services/api';

//Components
import Header from '../../Components/Header';
import Foooter from '../../Components/Footer';
import Sidebar from '../../Components/Sidebar'


//Css
import "../../Assets/Css/index.css";

export default function CadastrarUsuario() {

    //States
    const [nome, setNome] = useState("")
    const [email, setEmail] = useState("")
    const [senha, setSenha] = useState("")
    const [status, setStatus] = useState(true)
    const [tipoUsuario, setTipoUsuario] = useState(0)
    const [listaTiposUsuarios, setListaTiposUsuario] = useState([])
    const AvisoSucesso = () => toast.success('Usuário cadastrado com sucesso!');
    const AvisoErro = () => toast.error('Algo deu errado. Revise os campos do usuário!');

    const CadastrarUsuario = (event) => {

        event.preventDefault();

        var formData = new FormData();

        const element = document.getElementById('fotoCadastro')
        const file = element.files[0]

        if (element.value == "") {
            formData.append('FotoPerfil', null)

        } else {
            formData.append('FotoPerfil', file, file.name)

        }

        formData.append('Nome', nome);
        formData.append('Email', email);
        formData.append('Status', status);
        formData.append('Senha', senha);
        formData.append('IdTipoUsuario', tipoUsuario);

        api.post("/Usuarios/Cadastrar", formData, {

            Authorization: 'Bearer ' + localStorage.getItem('usuario-login'),
            headers: { "Content-Type": "multipart/form-data" }
        })
            .then((resposta) => {

                if (resposta.status === 201) {

                    AvisoSucesso();
                }
            })

            .catch(() => AvisoErro())

    }

    function BuscarTipoUsuario() {
        api.get('/TiposUsuarios/Listar', {
            headers: {

                Authorization: 'Bearer ' + localStorage.getItem('usuario-login')
            }
        }
        )
            .then((resposta) => {
                if (resposta.status === 200) {
                    setListaTiposUsuario(resposta.data)
                }
            })
            .catch(erro => console.log(erro))
    }

    useEffect(() => BuscarTipoUsuario(), [])

    return (
        <div>
            <Toaster
                position='top-right'
                toastOptions={{
                    style: {
                        fontFamily: "'Exo', sans-serif",
                        fontSize: "1rem",
                        fontWeight: 500
                    }
                }}
            />
            <Header />
            <Sidebar />
            {/* <!-- Main --> */}
            <main>
                <div className="container_cadastro_usuario container">
                    <form className='form_cadastro_usuario' onSubmit={CadastrarUsuario}>
                        <label htmlFor="fotoCadastro" className='label_cadastro_usuario'>Selecionar Foto</label>
                        <input
                            type="file"
                            className='input_cadastro_usuario'
                            id="fotoCadastro"
                            name="fotoCadastro"
                        />
                        <label></label>
                        <input type="text"
                            onChange={(event) => setNome(event.target.value)}
                            placeholder="Nome" value={nome} className="input_login"
                            required
                        />
                        <label></label>
                        <input type="text" placeholder="Email" value={email}
                            onChange={(event) => setEmail(event.target.value)}
                            className="input_login" 
                            required
                            />
                        <select
                            className="campo_tipo_usuario_cadastro_usuario"
                            name="tipoUsuario"
                            value={tipoUsuario}
                            onChange={(event) => setTipoUsuario(event.target.value)}
                            required
                        >
                            <option className="option_cadastro_usuario" value="0">Tipo Usuario</option>
                            {listaTiposUsuarios.map((tipoUsuario) => {
                                return <option value={tipoUsuario.idTipoUsuario}>{tipoUsuario.nomeTipoUsuario}</option>
                            }
                            )}
                        </select>

                        <select 
                        className="campo_tipo_usuario_cadastro_usuario" 
                        onChange={(event) => setStatus(event.target.value)}

                        >
                            <option className="option_cadastro_usuario" value="">Status</option>
                            <option value={true}>Ativo</option>
                            <option value={false}>Inativo</option>
                        </select>
                        <input type="text" placeholder="Senha" value={senha}
                            onChange={(event) => setSenha(event.target.value)}
                            className="input_login" 
                            required
                            />
                        <button type="submit" className="button_login">Cadastrar</button>
                    </form>
                </div>
            </main>
            <Foooter />
        </div>
    );
}