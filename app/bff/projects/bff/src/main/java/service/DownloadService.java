package service;

import config.Config;
import generateImageService.GenerateImageServiceGrpc;
import generateImageService.GenerateImageServiceOuterClass;
import interfaces.IDownloadService;
import io.grpc.stub.StreamObserver;
import mainBusiness.MainBusinessGrpc;
import mainBusiness.MainBusinessOuterClass;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.CountDownLatch;
import java.util.concurrent.TimeUnit;

/**
 * ダウンロードサービス
 */
@Service
public class DownloadService implements IDownloadService {
    /**
     * 画像一覧取得
     * @return 画像一覧
     */
    public List<MainBusinessOuterClass.PostImageFile> getPostImageList() throws InterruptedException {
        MainBusinessGrpc.MainBusinessStub stub = MainBusinessGrpc.newStub(Config.channel);
        MainBusinessOuterClass.EmptyRequest request = MainBusinessOuterClass.EmptyRequest.newBuilder().build();
        List<MainBusinessOuterClass.PostImageFile> postImageList = new ArrayList<MainBusinessOuterClass.PostImageFile>();
        CountDownLatch latch = new CountDownLatch(1);

        stub.getPostImageList(request, new StreamObserver<MainBusinessOuterClass.GetPostImageListReply>() {
            @Override
            public void onNext(MainBusinessOuterClass.GetPostImageListReply value) {
                MainBusinessOuterClass.PostImageFile postImageFile = value.getPostImageList();
                postImageList.add(postImageFile);
            }

            @Override
            public void onError(Throwable t) {
                t.printStackTrace();
                latch.countDown();
            }

            @Override
            public void onCompleted() {
                latch.countDown();
            }
        });

        latch.await(1, TimeUnit.MINUTES); // 非同期処理が完了するまで待機
        return postImageList;
    }

    /**
     * 画像生成ファイル取得
     * @return 生成画像
     */
    @Override
    public GenerateImageServiceOuterClass.ImageFile generateImage() {
        GenerateImageServiceGrpc.GenerateImageServiceBlockingStub stub = GenerateImageServiceGrpc.newBlockingStub(Config.aiChannel);
        GenerateImageServiceOuterClass.EmptyRequest request = GenerateImageServiceOuterClass.EmptyRequest.newBuilder().build();
        GenerateImageServiceOuterClass.ImageFile results = stub.generateImage(request);
        return results;
    }
}
