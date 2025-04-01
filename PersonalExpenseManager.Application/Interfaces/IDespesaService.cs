using System;
using PersonalExpanseManager.Entities;
using PersonalExpenseManager.Application.DTO;

namespace PersonalExpenseManager.Application.Interfaces;

public interface IDespesaService
{
    void AdicionarDespepsa(DespesaDTO despesa);
    List<DespesaDTO> ListarDespesas();
    List<DespesaDTO> FiltrarPorId(Guid id);
    List<DespesaDTO> FiltrarPorCategoria(string categoria);
    void DeletarDespesa(Guid id);
}
