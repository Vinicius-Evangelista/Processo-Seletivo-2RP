//verificar se o usuário está logado
export const usuarioAutenticado = () => localStorage.getItem('usuario-login') !== null;

//devolver token em JSON
export const parseJwt = () => {

    //base64 recebe o payload do token
    let base64 = localStorage.getItem('usuario-login').split('.')[1];

    //atob transforma base64 em string
    //JSON.parse pega a string e retorna em json.
    return JSON.parse(window.atob(base64))
}