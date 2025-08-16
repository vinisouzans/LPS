using AutoMapper;
using LPS.Data;
using LPS.DTOs.Cliente;
using LPS.Models;
using Microsoft.EntityFrameworkCore;

namespace LPS.Services
{
    public class ClienteService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ClienteService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClienteReadDTO>> ListarClientesAsync()
        {
            var clientes = await _context.Clientes.ToListAsync();
            return _mapper.Map<IEnumerable<ClienteReadDTO>>(clientes);
        }

        public async Task<ClienteReadDTO?> ObterClientePorIdAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            return cliente == null ? null : _mapper.Map<ClienteReadDTO>(cliente);
        }

        public async Task<ClienteReadDTO> CriarClienteAsync(ClienteCreateDTO dto)
        {
            // Validação: CPF já existe
            if (await _context.Clientes.AnyAsync(c => c.CPF == dto.CPF))
                throw new Exception("Já existe um cliente com este CPF.");

            var cliente = _mapper.Map<Cliente>(dto);
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return _mapper.Map<ClienteReadDTO>(cliente);
        }

        public async Task AtualizarClienteAsync(int id, ClienteUpdateDTO dto)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                throw new Exception("Cliente não encontrado.");

            // Verifica se o CPF já existe em outro cliente
            if (await _context.Clientes.AnyAsync(c => c.CPF == dto.CPF && c.Id != id))
                throw new Exception("Já existe outro cliente com este CPF.");

            _mapper.Map(dto, cliente);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarClienteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                throw new Exception("Cliente não encontrado.");

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }
    }
}
