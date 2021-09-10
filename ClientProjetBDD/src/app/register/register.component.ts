import { Component, Input, OnInit } from '@angular/core';
import { MBAlbum } from 'src/models/MusicBrainzModels/MBAlbum';
import { MBArtist } from 'src/models/MusicBrainzModels/MBArtist';
import { MusicBrainzProvider } from 'src/providers/MusicBrainzProvider';
import { SongsterrProvider } from 'src/providers/SongsterrProvider';

@Component({
	selector: 'app-register',
	templateUrl: './register.component.html',
	styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

	public artist: string;
	public album: string;
	public title: string;
	constructor(private mb: MusicBrainzProvider, private sngstrr: SongsterrProvider) { }

	ngOnInit(): void {

	}

	async submit() {
		try {
			var artistSearchResults: MBArtist[] = await this.mb.getArtists(this.artist);
			if (artistSearchResults.length == 0) {
				alert(`L'ariste '${this.artist}' est introuvable`);
				return;
			}
			var chosenArtist: MBArtist = artistSearchResults[0];
			var albumSearchResult: MBAlbum[] = await this.mb.getAlbums(chosenArtist.id);
			if (albumSearchResult.length == 0) {
				alert(`L'album '${this.album}' est introuvable pour l'artiste '${this.artist}'`);
				return;
			}
			var chosenAlbum: MBAlbum = albumSearchResult[0];
			var songId: number = await this.sngstrr.getSongTabId(this.artist, this.title);
			// TODO: Appeler le backend pour enregistrer notre base
		}
		catch (e) {
			this.artist = "";
			this.album = "";
			this.title = "";
			alert("Une erreur est survénue durant la recherche du titre demandé: " + e.message);
			
		}
	}

}
