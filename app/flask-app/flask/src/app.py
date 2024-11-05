from flask import Flask
from flask_cors import CORS
import psycopg2
import psycopg2.extras

app = Flask(__name__)
CORS(app)

@app.route("/")
def hello():
    dsn = "postgresql://postgres:password@postgres:5432/postgres"

    conn = psycopg2.connect(dsn)
    cur = conn.cursor(cursor_factory=psycopg2.extras.DictCursor)
    
    cur.execute("select * from test_users")
    results = cur.fetchall()
    dict_result = []
    for row in results:
        dict_result.append(dict(row))
    cur.close()
    conn.close()

    return dict_result

if __name__ == "__main__":
    app.run(debug=True, host="0.0.0.0", port=5000)
