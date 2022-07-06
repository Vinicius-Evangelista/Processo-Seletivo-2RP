﻿using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Http.Headers;

namespace _2rp_processo.webApi.Utils
{
    public class Upload
    {
        /// <summary>
        /// Faz o upload do arquivo para o servidor
        /// </summary>
        /// <param name="arquivo">Arquivo vindo de um formulário</param>
        /// <returns>Nome do arquivo salvo</returns>
        public static string UploadFile(IFormFile arquivo)
        {
            try
            {
                var pasta = Path.Combine("StaticFiles", "UserImages");
                var caminho = Path.Combine(Directory.GetCurrentDirectory(), pasta);

                string[] extensoesPermitidas = { "jpg", "png", "jpeg" };


                if (arquivo.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(arquivo.ContentDisposition).FileName.Trim('"');

                    if (ValidarExtensao(extensoesPermitidas, fileName))
                    {
                        var extensao = RetornarExtensao(fileName);
                        var novoNome = $"{Guid.NewGuid()}.{extensao}";
                        var caminhoCompleto = Path.Combine(caminho, novoNome);

                        using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                        {
                            arquivo.CopyTo(stream);
                        }

                        return novoNome;
                    }

                    return "Extensão não permitida";
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        /// <summary>
        /// Valida o uso de enxtensões permitidas apenas
        /// </summary>
        /// <param name="extensoes">Array de extensões permitidas</param>
        /// <param name="nomeDoArquivo">Nome do arquivo</param>
        /// <returns>Verdadeiro/Falso</returns>
        public static bool ValidarExtensao(string[] extensoes, string nomeDoArquivo)
        {
            string[] dados = nomeDoArquivo.Split(".");
            string extensao = dados[dados.Length - 1];

            foreach (var item in extensoes)
            {
                if (extensao == item)
                {
                    return true;
                }
            }
            return false;
        }

        public static string AtualizarArquivo(string nomeArquivoAntigo, IFormFile novoArquivo)
        {
            RemoverArquivo(nomeArquivoAntigo);
            string nomeArquivo = UploadFile(novoArquivo);
            return nomeArquivo;
        }

        /// <summary>
        /// Remove um arquivo do servidor
        /// </summary>
        /// <param name="nomeDoArquivo">Nome do Arquivo</param>
        public static void RemoverArquivo(string nomeDoArquivo)
        {
            var pasta = Path.Combine("StaticFiles", "UserImages");
            var caminho = Path.Combine(Directory.GetCurrentDirectory(), pasta);
            var caminhoCompleto = Path.Combine(caminho, nomeDoArquivo);

            File.Delete(caminhoCompleto);
        }

        /// <summary>
        /// Retorna a extensão de um arquivo
        /// </summary>
        /// <param name="nomeDoArquivo">Nome do Arquivo</param>
        /// <returns>Retorna a extensão de um arquivo</returns>
        public static string RetornarExtensao(string nomeDoArquivo)
        {
            string[] dados = nomeDoArquivo.Split(".");
            return dados[dados.Length - 1];
        }

    }
}
