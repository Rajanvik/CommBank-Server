import os
import glob
from pathlib import Path
from pymongo import MongoClient
from bson import json_util

def get_mongo_uri():
    env_paths = [Path(".env"), Path("../.env"), Path("server/.env")]
    for path in env_paths:
        if path.exists():
            with open(path, "r") as f:
                for line in f:
                    line = line.strip()
                    if line.startswith("MONGODB_URI=") or line.startswith("MONGO_URI="):
                        return line.split("=", 1)[1].strip()
    return os.environ.get("MONGODB_URI") or os.environ.get("MONGO_URI")

def seed():
    uri = get_mongo_uri()
    if not uri:
        raise ValueError("MongoDB URI not found in .env or environment variables.")

    print("Connecting to MongoDB...")
    client = MongoClient(uri)
    db = client["CommBank"]

    data_dir = Path("data")
    if not data_dir.exists():
        data_dir = Path("../data")

    files = sorted(data_dir.glob("*.json"))
    if not files:
        print("No JSON files found in data directory.")
        return

    for file_path in files:
        collection_name = file_path.stem  # e.g., 'Accounts', 'Goals', etc.
        collection = db[collection_name]

        with open(file_path, "r", encoding="utf-8") as f:
            documents = json_util.loads(f.read())

        if documents:
            # Clear existing seed data to prevent duplicate key errors
            collection.delete_many({})
            result = collection.insert_many(documents)
            print(f"✓ [{collection_name}]: Inserted {len(result.inserted_ids)} documents.")
        else:
            print(f"- [{collection_name}]: Empty file, skipped.")

    print("\nDatabase 'CommBank' successfully seeded!")

if __name__ == "__main__":
    seed()
