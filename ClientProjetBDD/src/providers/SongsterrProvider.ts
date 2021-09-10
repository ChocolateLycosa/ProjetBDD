import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
@Injectable()
export class SongsterrProvider {
    private baseUrl = "http://www.songsterr.com/a/ra/";
    constructor(private http: HttpClient){
        
    }

    public async getSongTabId(artistName: string, songName: string) : Promise<number> {
        var getSongsUrl: string = `${this.baseUrl}songs/byartists.json?artists=${artistName}`;
        var result = await this.http.get(getSongsUrl).toPromise<any>();
        result = result.filter(t => t.title == songName);
        if (result.length == 0){
            throw new Error("Aucune tablature trouvée pour ce titre");
        }
        return result[0].id;
    }
}