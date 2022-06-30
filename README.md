<p align="center">
  <img src="https://i.imgur.com/riwbnp5.png" />
</p>
<h1 align="center">
  Comment Participation System
</h1>
<h2>
  Meta
</h2>
<h4>
  Project Version: 1.0.0
  <br><br>
  Unity Version: 2021.3.5f1
</h4>
<h2>
  Functionality
</h2>
To comment or rate an object you have to make the object observable. To do that the object must have an ObservableObject script attached to it. Once that is done the player is then able to interact with that object and place a contribution, by either leaving some comments or ratings.
<br>
The player is able to leave an unlimited amount of comments and ratings on an observable.
<br><br>
The player is also able to create spot observables. The spot observables can be placed anywhere on any object. Once the spot observable is placed it will appear as a red marker in 3D space.
<br><br>
<img src="https://i.imgur.com/L0abnqh.png" width="32" height="49">

<h2>
  Interaction
</h2>
To interact with an observable the player has to be close by to the target object and press a Left Mouse Button. Once the player interacts with an observable, the main feedback UI window will appear.
<br><br>
To create a spot observable, a player has to target a surface at close range and press Right Mouse Button.
<h2>
  Contribution
</h2>
<h3>
  Comment
</h3>
The comment is text-based feedback that contains the following attributes: contribution type, title, and comment.
<br><br>
There are three contribution types:
<br><br>
<ul>
  <li>Suggestion</li>
  <li>Criticism</li>
  <li>Opinion</li>
</ul>
<h3>
  Rating
</h3>
The rating is score-based feedback that contains the following attributes: title and a score rating that goes from 1 to 5.
<h2>
  Data Persistence
</h2>
The contribution data is saved into a JSON file. The save data event is triggered whenever the feedback is created, changed, and deleted. To save the data a component SaveLoad needs to be attached to the game object. By doing so an additional component will be added if not added already, which is Registry.
<br><br>
The registry is a class that stores all the observable objects. Once the SaveLoad class tries to save the data, the Data class is fetched from the Registry class that contains all the contribution data.
<br><br>
The data is then loaded at the start of the game. The data is stored in a JSON file that has a name equal to the name given in the active user profile.
<h2>
  External Integration
</h2>
It is easy to integrate this system into an external project. Once the project files are imported to a Unity project, the only thing that needs to be done is to assign a profile to the GameManager by calling SetProfile(Profile profile) event that is located in the GlobalEvents class.
<br><br>
Once that is done the system should work out of the box. The main requirements that need to exist in the scene for the system to function are the core UI system, GameManager, and an Interaction system.
<br><br>
Having these things in the scene will allow the player to interact with all objects that have an Observable component.
