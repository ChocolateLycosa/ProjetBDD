import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DashboardComponent } from './dashboard/dashboard.component';
import { RegisterComponent } from './register/register.component';
import { LibraryComponent } from './library/library.component';
import { MenuComponent } from './menu/menu.component';
import { SongsterrProvider } from 'src/providers/SongsterrProvider';
import { MusicBrainzProvider } from 'src/providers/MusicBrainzProvider';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    RegisterComponent,
    LibraryComponent,
    MenuComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    {
      provide: SongsterrProvider,
      useClass: SongsterrProvider
    },
    {
      provide: MusicBrainzProvider,
      useClass: MusicBrainzProvider
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
