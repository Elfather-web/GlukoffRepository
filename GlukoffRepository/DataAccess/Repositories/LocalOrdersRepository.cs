using GlukoffRepository.Abstraction;
using GlukoffRepository.DataAccess;

namespace GlukoffRepository.Services;

public class LocalOrdersRepository(string connection): SqliteRepository<LocalOrder>(connection);