backend-dev:
	cd ./app/backend/ExRates.Api && dotnet run

frontend-dev:
	cd ./app/frontend/ && npm run start

prod:
	docker compose up -d

prod-stop:
	docker compose down
