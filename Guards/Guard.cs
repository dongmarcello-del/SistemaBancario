/// <summary>
/// Queste classi sono ritenute parti del servizio,
/// lanciano errori e fanno controlli dei dati dell'utente lato DB
/// 
/// Se riscontrano errori -> lanciano eccezioni
/// Se non riscontrano errori -> ritornano il dato richiesto
/// 
/// Controller (ResponseMessage) <- Service (Error/Dato) / Guard (Error/Dato) <- Validator (ResponseMessage)
/// 
/// Il Guard (In questo progetto) garantisce che i requisiti siano validi prima di eseguire un'operazione
/// 
/// Ex. Voglio vedere se le credenziali sono corrette
/// 
/// Operazione finale:
/// Confrontare la password inserita e quella dell'utente reale
/// 
/// Quali sono i requisiti?
/// Che l'email esista -> va nel guard
/// 
/// </summary>

namespace SistemaBancario.Guards
{
    /// <summary>
    /// 
    /// Guard.Check(DTO) -> datoDB or Eccezione
    /// 
    /// </summary>
}