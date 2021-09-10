import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { MBArtist } from "src/models/MusicBrainzModels/MBArtist";
import { MBAlbum } from "src/models/MusicBrainzModels/MBAlbum";
@Injectable()
export class MusicBrainzProvider {
    private baseUrl: string = "https://api.musixmatch.com/ws/1.1/";
    constructor(private http: HttpClient){
    }


    public getArtists(artistName: string) : Promise<MBArtist[]> {
        var finalUrl: string = `${this.baseUrl}artist.search`;
        var params: HttpParams = new HttpParams();
        params = params.set("apikey", "7297745482604b5f4a1a990b07c42860");
        params = params.set("q_artist", artistName);
        params = params.set("format", "json");
        var options = {
            params: params,
            headers: this.headers()
        }
        var result = this.http.get(finalUrl, options).toPromise<any>();
        var artistList: MBArtist[] = [];
        result
            .artist_list
            .filter(t => t.artist.artist_name == artistName)
            .forEach(t => {
                artistList.push(MBArtist.Parse(t.artist))
            });
        return artistList;
    }


    public async getAlbums(artistId: number) : Promise<MBAlbum[]> {
        var finalUrl: string = `${this.baseUrl}artist.album.get?apikey=${"7297745482604b5f4a1a990b07c42860"}&artist_id=${artistId}`
        var result = await this.http.get(finalUrl).toPromise<any>();
        var albumList: MBAlbum[] = [];
        result
            .album_list
            .forEach(t => {
                albumList.push(MBAlbum.Parse(t.album))
            });
        return albumList;
    }

    private headers(): HttpHeaders {
        var headers: HttpHeaders = new HttpHeaders();
        headers = headers.set("Connexion", "keep-alive");
        headers = headers.set("Content-Type", "application/json");
        headers = headers.set("Accept", "*/*");
        headers = headers.set("Accept-Encoding", "*/*");
        headers = headers.set("Authentication", "Bearer 7297745482604b5f4a1a990b07c42860");
        headers = headers.set("User-Agent", window.navigator.userAgent)
        headers = headers.set("Access-Control-Allow-Origin", "*");
        return headers;
    }
}