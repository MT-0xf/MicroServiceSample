<template>
  <div class="main">
    <h1 class="title">画像登録</h1>
    <div class="inputArea">
      <label class="inputLabel">タイトル</label>
      <input type="text" v-model="title">
    </div>
    <div class="inputArea">
      <label class="inputLabel">ファイル</label>
      <input type="file" @change="onImageUpload">
    </div>
    <div class="submitArea">
      <button class="submitButton" @click="postImage">保存</button>
    </div>
    <div class="submitArea">
      <button class="submitButton" @click="navigate">画像一覧へ移動</button>
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

  /** 画像登録クエリ */
  const mutation = gql`
    mutation postImage($title: String, $imageFile: String) {
      postImage(title: $title, imageFile: $imageFile) {
        Message
      }
    }
  `;

  // タイトル
  let title = ref("");

  // 画像ファイル
  let imageFile: string | ArrayBuffer | null;

  /** ルーター */
  const router = useRouter();

  /** 画像アップロード */
  const onImageUpload = (e: Event) => {
    const files = (e.target as HTMLInputElement).files;
    if (files && files.length > 0) {
      createImage(files[0])
    }
  }

  /** 画像ファイル作成 */
  const createImage = (image: File) => {
    const reader = new FileReader()
    // imageをreaderにDataURLとしてattachする
    reader.readAsDataURL(image)
    // readAdDataURLが完了したあと実行される処理
    reader.onload = () => {
      const result = reader.result;
      imageFile = result;
    }
  }

  /** 画像登録 */
  const postImage = () => {
    if (title.value == "") {
      window.alert("タイトルを入力してください");
      return;
    }

    if (!imageFile) {
      window.alert("画像をアップロードしてください");
      return;
    }
    defaultClient
    .mutate({
      mutation: mutation,
      variables: {
        title: title.value,
        imageFile: imageFile
      }
    })
    .then((res: any) => {
      console.log(res);
    });
  }

  /** 画像一覧へ移動 */
  const navigate = () => {
    router.push("/postImageList");
  }
</script>

<style scoped>
.main {
  width: 400px;
  margin: 16px auto;
}

.title {
  padding: 0 20px;
}

.inputArea {
  padding: 5px;
}

.inputLabel {
  padding: 20px;
}

.submitArea {
  padding: 20px;
  width: 350px
}

.submitButton {
  width: 100%;
}
</style>