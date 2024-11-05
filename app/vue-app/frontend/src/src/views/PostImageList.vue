<template>
  <div class="main">
    <h1 class="title">画像一覧</h1>
    <div class="grid-container">
      <div class="grid">
        <li class="grid-item" v-for="file in fileList" :key="file.Content">
          <img class="thumbnail" :src="'data:image/jpeg;base64,' + file.Content" />
        </li>
      </div>
    </div>
    <div>
      <button class="button" @click="getImage">画像生成ダウンロード</button>
      <button class="button" @click="navigate">画像登録へ移動</button>
    </div>
  </div>
</template>

<script setup lang="ts">
  import { ApolloClient, gql, InMemoryCache } from '@apollo/client';
  import { ref } from 'vue';
  import { useRouter } from 'vue-router';

  /** GraphQLクライアント */
  const defaultClient = new ApolloClient({
    uri: 'http://localhost:8080/graphql',
    cache: new InMemoryCache()
  });

  // 画像一覧
  let fileList = ref();

  /** ルーター */
  const router = useRouter();

  /** 画像一覧取得クエリ */
  const query = gql`
    query getPostImageList {
      getPostImageList {
        Content
        ContentType
      }
    }  
  `;

  /** 初期表示のため画像一覧取得 */
  defaultClient
  .query({
    query: query,
  })
  .then((res: any) => {
    if (res.data.getPostImageList) {
      fileList.value = res.data.getPostImageList;
    }
  });

  const getImage = () => {
    /** 画像一覧取得クエリ */
    const generateImageQuery = gql`
      query {
        generateImage{
          Content
          ContentType
        }
      }
    `;
    defaultClient
    .query({
      query: generateImageQuery,
    })
    .then((res: any) => {
      if (res.data.generateImage) {
        // ここにBase64エンコードされた画像データを入力
        const base64Data = res.data.generateImage.Content.split(",")[1];
        
        // MIMEタイプを指定
        const mimeType = "image/jpeg";
        
        // Base64文字列をBlobに変換
        const byteCharacters = atob(base64Data);
        const byteNumbers = new Array(byteCharacters.length);
        for (let i = 0; i < byteCharacters.length; i++) {
            byteNumbers[i] = byteCharacters.charCodeAt(i);
        }
        const byteArray = new Uint8Array(byteNumbers);
        const blob = new Blob([byteArray], { type: mimeType });
        
        // Blob URLを生成
        const blobUrl = URL.createObjectURL(blob);
        
        // ダウンロードリンクを生成
        const link = document.createElement('a');
        link.href = blobUrl;
        link.download = 'image.jpg'; // ダウンロードするファイル名
        link.click();
        
        // メモリリークを防ぐためにBlob URLを解放
        URL.revokeObjectURL(blobUrl);
      }
    });
  }

  /** 画像登録へ移動 */
  const navigate = () => {
    router.push("/upload");
  }

</script>

<style scoped>
.main {
  width: 80%;
  margin: 16px auto;
}

.title {
  padding: 0 20px;
}

.grid-container {
  width: 100%;
}

.grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, 300px);
  gap: 16px;
}
.grid-item {
  list-style-type: none;
}

.thumbnail {
  width: 300px;
  height: auto;
}

.button {
  margin-right: 10px;
}
</style>