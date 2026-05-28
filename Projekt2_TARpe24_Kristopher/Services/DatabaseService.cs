using Projekt2_TARpe24_Kristopher.Models;
using SQLite;

namespace Projekt2_TARpe24_Kristopher.Services;

public class DatabaseService
{
    private SQLiteAsyncConnection? database;

    private async Task Init()
    {
        if (database is not null)
        {
            return;
        }

        SQLitePCL.Batteries_V2.Init();
        var path = Path.Combine(FileSystem.AppDataDirectory, "cityexplorer.db3");
        database = new SQLiteAsyncConnection(path);
        await database.CreateTableAsync<Place>();
    }

    public async Task<List<Place>> GetFavoritesAsync()
    {
        await Init();
        return await database!.Table<Place>().Where(place => place.IsFavorite).ToListAsync();
    }

    public async Task SaveFavoriteAsync(Place place)
    {
        await Init();
        var favorite = place.Copy();
        favorite.IsFavorite = true;
        await database!.InsertOrReplaceAsync(favorite);
    }

    public async Task DeleteFavoriteAsync(Place place)
    {
        await Init();
        await database!.DeleteAsync(place);
    }
}
