Server-Side Web MVC Framework Term Project
	December 5, 2025

Team Members:
	Brooke Wilson
	Brittany Wilcox

Admin credentials:
	Username: user
	Pw: admin123




Public Features (no login required):
	- View list of paid teams
	- View team details
	- Register a new team

Admin Features (login required):
	- View all teams (paid and unpaid)
	- Filter teams by payment status
	- Filter teams by divison
	- Mark teams as paid
	- View registration summary and total amount of fees collected
	- Add, edit, and delete teams

Project Structure:
	We began this project based on the specs in the first version of the project details, which did not
	include the actual database. As a result, our initial implementation did not account for the BowlingUser
	table or the division table. We also began the project well before the suggested controller structure
	and naming conventions were provided, so a lot of the controller names / property names etc differ from
	what was suggested later on.

Notes:
	UserController and User view pages refer to the Admin features that are only accessible after logging in.
	TournamentController and Tournament view pages refer to the publicly accessible pages (no login required).

Known Bugs:
	- If an unauthenticated user tries to navigate to any of the User/____ pages, they are routed to the login page instead of the access denied page.
	- No error handling when an unauthenticated user types a nonexistent ID into the URL